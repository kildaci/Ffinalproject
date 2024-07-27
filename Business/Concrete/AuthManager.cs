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
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }
        public IResult ResetPassword(string userName)
        {
            var user = _userService.GetByMail(userName);
            if (user == null)
                return new ErrorResult("User Not Found");

            string newPassword = CoreHelpers.CreatePassword(8);
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(newPassword, out passwordHash, out passwordSalt);
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            user.Status = true;
            _userService.Update(user);

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

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            throw new NotImplementedException();
        }

        //public IDataResult<AccessToken> CreateAccessToken(User user)
        //{
        //    var claims = _userService.GetClaims(user);
        //    var accessToken = _tokenHelper.CreateToken(user, claims);
        //    return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        //}
    }
}
