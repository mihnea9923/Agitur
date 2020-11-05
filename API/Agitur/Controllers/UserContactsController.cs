using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agitur.APIModel.Contacts;
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
        public IEnumerable<UserContact> GetUserContacts()
        {
            //get the user who issued the request
            string userId = User.Claims.First(o => o.Type == "UserId").Value;
            //var agiturUser = userManager.FindByIdAsync(userId).GetAwaiter().GetResult();
            User user = userServices.GetById(Guid.Parse(userId));
            IEnumerable<User> userContacts = userContactsServices.GetUserConctacts(user.Id);
            List<UserContact> model = new List<UserContact>();
            foreach(var userContact in userContacts)
            {
                UserContact temp = new UserContact()
                {
                    Id = userContact.Id,
                    ProfilePhoto = userContact.ConvertPhotoToBase64(),
                    Message = "",
                    MessageRead = true,
                    MessageTime = "12:33",
                    Name = userContact.Name
                };
                model.Add(temp);
            }
            
            return model.AsEnumerable();
        }

    }
}
