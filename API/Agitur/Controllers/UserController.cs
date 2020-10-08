using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agitur.APIModel;
using Agitur.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Agitur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AgiturUser> userManager;
        private readonly SignInManager<AgiturUser> signInManager;

        public UserController(UserManager<AgiturUser> userManager , SignInManager<AgiturUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
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
          
            try
            {
                var result = await userManager.CreateAsync(user, model.Password);
                return Ok(result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
