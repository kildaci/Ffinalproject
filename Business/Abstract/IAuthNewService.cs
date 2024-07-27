using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthNewService
    {
        IDataResult<UserNew> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<UserNew> Login(UserForLoginDto userForLoginDto);
        IDataResult<AccessToken> CreateAccessToken(UserNew userNew);
        IResult UserExists(string email);
        IResult ResetPassword(string userName);
        IDataResult<UserNew> Logout(UserForLoginDto userForLoginDto);

    }
}
