using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agitur.APIModel.UserProfile;
using Agitur.ApplicationLogic;
using Agitur.Identity;
using Agitur.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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

            var photo = Request.Form.Files[0];
            if (photo != null)
            {
                userServices.UpdateProfilePhoto(Guid.Parse(userId), photo);
                return Ok("Photo updated");
            }

            return BadRequest("Something went wrong on the server");

        }
        [HttpGet]
        [Route("profilePhoto")]
        [Authorize]
        public UserProfilePhotoModel GetUserProfilePhoto()
        {
            string userId = User.Claims.First(o => o.Type == "UserId").Value;
            User user = userServices.GetById(Guid.Parse(userId));
            UserProfilePhotoModel result = new UserProfilePhotoModel();
            if(user.ProfilePhoto != null)
            {
                result.ProfilePhoto = user.ConvertPhotoToBase64();
            }
            return result;
        }
    }
}
