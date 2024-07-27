using Business.Abstract;
using Business.Constants;
using Business.Mail;
using Core.Entities.Concrete;
using Core.Helper;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.Concrete.Email;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthNewManager : IAuthNewService
    {
        private IUserNewService _userNewService;
        private ITokenHelper _tokenHelper;

        public AuthNewManager(IUserNewService userNewService, ITokenHelper tokenHelper)
        {
            _userNewService = userNewService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<AccessToken> CreateAccessToken(UserNew userNew)
        {
            var claims = _userNewService.GetClaims(userNew);
            var accessToken = _tokenHelper.CreateToken(userNew,claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public IDataResult<UserNew> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var EmailCheck= _userNewService.GetByMail(userForRegisterDto.Email);
            if (EmailCheck != null)
            {
                return new ErrorDataResult<UserNew>(null, "Bu e-posta adresi zaten kullanılıyor.");
            }
            
            var userNew = new UserNew
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userNewService.Add(userNew);
            return new SuccessDataResult<UserNew>(userNew, "");
 
        }

        public IDataResult<UserNew> Login(UserForLoginDto userForLoginDto)
        {
            var userNewToCheck = _userNewService.GetByMail(userForLoginDto.Email);
            if (userNewToCheck == null)
            {
                return new ErrorDataResult<UserNew>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userNewToCheck.PasswordHash, userNewToCheck.PasswordSalt))
            {
                return new ErrorDataResult<UserNew>(Messages.PasswordError);
            }

            return new SuccessDataResult<UserNew>(userNewToCheck, Messages.SuccessfulLogin);
        }

        public IResult UserExists(string email)
        {
            if (_userNewService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public IResult ResetPassword(string userName)
        {


            var user = _userNewService.GetByMail(userName);
            if (user == null)
                return new ErrorResult("User Not Found");

            string newPassword = CoreHelpers.CreatePassword(8);
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(newPassword, out passwordHash, out passwordSalt);
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            user.Status = true;
           // _userNewService.Update(user);

            var emailMessage = new EmailMessage
            {
                ToAddresses = new List<EmailAddress> { new EmailAddress() { Address = user.Email, Name = user.FirstName } },
                Subject = "Şifreniz Güncelledi!",
                Content = $"Selam {user.FirstName}, Finder şifreni sıfırlandı, yeni şifreniz <b>{newPassword}</b>"
            };
            MailManager mailManager = new MailManager();
            mailManager.Send(emailMessage);
            return new SuccessResult("Şifreniz Güncelledi!");
        }

        public IDataResult<UserNew> Logout(UserForLoginDto userForLoginDto)
        {
            throw new NotImplementedException();
        }
    }
}
