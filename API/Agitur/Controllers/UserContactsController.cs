using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agitur.ApplicationLogic;
using Agitur.Identity;
using Agitur.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Agitur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserContactsController : ControllerBase
    {
        private readonly UserContactsServices userContactsServices;
        private readonly UserServices userServices;
        private readonly UserManager<AgiturUser> userManager;

        public UserContactsController(UserContactsServices userContactsServices , UserServices userServices ,
            UserManager<AgiturUser> userManager) 
        {
            this.userContactsServices = userContactsServices;
            this.userServices = userServices;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<User> GetUserContacts()
        {
            //get the user who issued the request
            string userId = User.Claims.First(o => o.Type == "UserId").Value;
            //var agiturUser = userManager.FindByIdAsync(userId).GetAwaiter().GetResult();
            var user = userServices.GetById(Guid.Parse(userId));
            return userContactsServices.GetUserConctacts(user.Id);
        }
    }
}
