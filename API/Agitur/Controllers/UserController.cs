using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Agitur.APIModel;
using Agitur.ApplicationLogic;
using Agitur.DataAccess.Abstractions;
using Agitur.Identity;
using Agitur.Model;
using Agitur.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Agitur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AgiturUser> userManager;
        private readonly SignInManager<AgiturUser> signInManager;
        private readonly UserServices userServices;
        private readonly ApplicationSettings appSettings;

        public UserController(UserManager<AgiturUser> userManager , SignInManager<AgiturUser> signInManager,
            IOptions<ApplicationSettings> appSettings , UserServices userServices)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.userServices = userServices;
            this.appSettings = appSettings.Value;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<Object> PostUser(AgiturUserModel model)
        {
            var user = new AgiturUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName

            };
            User myUser = new User()
            {
                AgiturUser = user.Id,
                Email = user.Email,
                Name = user.UserName
                };
            try
            {
                var result = await userManager.CreateAsync(user, model.Password);
                if(result.Succeeded == true)
                userServices.CreateUser(myUser);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        [HttpPost]
        [Route("Login")]
        //POST: /api/User/Login
        public async Task<IActionResult> LogIn(LogInModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if(user != null && await userManager.CheckPasswordAsync(user ,model.Password))
            {
                var agiturUser = userServices.GetByUserId(user.Id);
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("AgiturId", user.Id.ToString()),
                        new Claim("Email" , user.Email),
                        new Claim("UserId" , agiturUser.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(appSettings.JWT_Secret)),SecurityAlgorithms.HmacSha256Signature)
                };
                
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new {token });
            }
            else
            {
                return BadRequest(new { message = "Email or password is incorrect" });
            }
        }
        [HttpGet]
        [Route("findFriends/{email}")]
        [Authorize]
        public IEnumerable<AgiturUser> FindFriends(string email)
        {
            var users = userManager.Users.Where(user => user.Email.ToLower().Contains(email.ToLower()));
            return users;
        }
    }
}
