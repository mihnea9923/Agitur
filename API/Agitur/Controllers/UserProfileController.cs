using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agitur.ApplicationLogic;
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
        private readonly UserServices userServices;

        public UserProfileController(UserManager<AgiturUser> userManager, UserServices userServices)
        {
            this.userManager = userManager;
            this.userServices = userServices;
        }

        [HttpGet]
        [Authorize]
        //GET: api/UserProfile
        public async Task<Object> GetUserProfile()
        {
            //get the current user
            string userId = User.Claims.First(c => c.Type == "AgiturId").Value;
            var user = await userManager.FindByIdAsync(userId);

            return new
            {
                FullName = user.FullName,
                Email = user.Email,
                Name = user.UserName
            };
        }
        [HttpPost]
        [Authorize]
        [Route("uploadProfilePhoto")]
        public IActionResult UpdateProfilePhoto()
        {
            //get agitur id from the user who issues the request
            string userId = User.Claims.First(o => o.Type == "UserId").Value;
            try
            {
                var photo = Request.Form.Files[0];
                userServices.UpdateProfilePhoto(Guid.Parse(userId) , photo);
                return Ok("Photo updated");
            }
            catch(Exception e)
            {
                return BadRequest("Something went wrong on the server");
            }

        }
    }
}
