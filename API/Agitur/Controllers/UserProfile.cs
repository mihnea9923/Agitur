using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agitur.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Agitur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly UserManager<AgiturUser> userManager;

        public UserProfileController(UserManager<AgiturUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        //GET: api/UserProfile
        public async Task<Object> GetUserProfile()
        {
            //get the current user
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            var user = await userManager.FindByIdAsync(userId);

            return new
            {
                FullName = user.FullName,
                Email = user.Email,
                Name = user.UserName
            };
        }
    }
}
