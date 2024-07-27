using Business.Abstract;
using Entities.DTOs;
using Entities.DTOs.Email;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthNewController : ControllerBase
    {
        private IAuthNewService _authNewService;
        

        public AuthNewController(IAuthNewService authNewService)
        {
            _authNewService = authNewService;
            
        }

     

        [HttpPost("register")]
        public ActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authNewService.UserExists(userForRegisterDto.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }

            var registerResult = _authNewService.Register(userForRegisterDto, userForRegisterDto.Password);
            var result = _authNewService.CreateAccessToken(registerResult.Data);
            if (registerResult.Success)
            {
                return Ok(registerResult);
            }

            return BadRequest("");
        }
        [HttpPost("login")]
        public ActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authNewService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = _authNewService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        //[HttpPost("logout")]
        //public IActionResult Logout()
        //{

        //    HttpContext.SignOutAsync(); 


        //    return Ok("Çıkış işlemi başarılı."); 
        //}
        

        [HttpPost("logout")]
        public ActionResult Logout()
        {

            HttpContext.Session.Clear();


            return RedirectToAction("Index", "Home");
            //return Ok("Çıkış işlemi başarılı.");
        }

        [HttpPost("resetPassword")]
        public ActionResult resetPassword(ResetPasswordDto resetPasswordDto)
        {


            var registerResult = _authNewService.ResetPassword(resetPasswordDto.Email);
            if (registerResult.Success)
            {
                return Ok(registerResult.Message);
            }

            return BadRequest(registerResult.Message);
        }

        [Authorize]
        [HttpGet("checkLoginStatus")]
        public IActionResult CheckLoginStatus()
        {
            
            return Ok("Kullanıcı giriş yapmış.");
        }


    }
}
