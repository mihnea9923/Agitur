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
    [Authorize]
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
        public IEnumerable<UserContactViewModel> GetUserContacts()
        {
            //get the user who issued the request
            string userId = User.Claims.First(o => o.Type == "UserId").Value;
            //var agiturUser = userManager.FindByIdAsync(userId).GetAwaiter().GetResult();
            User user = userServices.GetById(Guid.Parse(userId));
            IEnumerable<User> userContacts = userContactsServices.GetUserConctacts(user.Id);
            List<UserContactViewModel> model = new List<UserContactViewModel>();
            foreach(var userContact in userContacts)
            {
                UserContactViewModel temp = new UserContactViewModel()
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
        [HttpPost]
        public IActionResult CreateUserContact(NewUserContactViewModel model)
        {
            string userId = User.Claims.First(o => o.Type == "UserId").Value;
            User user1 = userServices.GetById(Guid.Parse(userId));
            User user2 = userServices.GetById(model.User2Id);
            UserContacts userContact = new UserContacts()
            {
                User1 = user1,
                User2 = user2,
                Position = user1.ContactsNumber
            };
            userContactsServices.AddContact(userContact);
            return Ok("New contact added");
        }
    }
}
