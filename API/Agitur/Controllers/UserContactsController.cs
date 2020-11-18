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
        private readonly MessageServices messageServices;

        public UserContactsController(UserContactsServices userContactsServices, UserServices userServices,
            UserManager<AgiturUser> userManager, MessageServices messageServices)
        {
            this.userContactsServices = userContactsServices;
            this.userServices = userServices;
            this.userManager = userManager;
            this.messageServices = messageServices;
        }

        [HttpGet]
        public IEnumerable<UserContactViewModel> GetUserContacts()
        {
            //get the user who issued the request
            string userId = User.Claims.First(o => o.Type == "UserId").Value;
            Guid userIdGuid = Guid.Parse(userId);
            User user = userServices.GetById(userIdGuid);
            IEnumerable<User> userContacts = userContactsServices.GetUserConctacts(user.Id);
            List<UserContactViewModel> model = new List<UserContactViewModel>();
            int position = 0;
            foreach (var userContact in userContacts)
            {
                Message lastMessage = messageServices.GetLastMessage(userIdGuid, userContact.Id);
                UserContactViewModel temp = new UserContactViewModel()
                {
                    Id = userContact.Id,
                    ProfilePhoto = userContact.ConvertPhotoToBase64(),
                    Message = lastMessage.Text,
                    MessageRead = lastMessage.Read,
                    MessageTime = lastMessage.Date,
                    Name = userContact.Name,
                    Received = lastMessage.SenderId == userIdGuid ? false : true,
                    Position = position
                };
                model.Add(temp);
                position++;
            }

            return model.AsEnumerable();
        }

        [HttpPost]
        public IActionResult CreateUserContact(ContactId model)
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
        [HttpGet]
        [Route("getContact/{id}")]
        public UserContactViewModel GetContactById(Guid id)
        {
            Guid userId = Guid.Parse(User.Claims.First(o => o.Type == "UserId").Value);
            User contact = userServices.GetById(id);
            Message lastMessage = messageServices.GetLastMessage(userId, id);
            UserContactViewModel userContactViewModel = new UserContactViewModel()
            {
                Id = id,
                Message = lastMessage.Text,
                MessageRead = lastMessage.Read,
                MessageTime = lastMessage.Date,
                Name = contact.Name,
                ProfilePhoto = contact.ConvertPhotoToBase64(),
                Received = lastMessage.SenderId == userId ? false : true
            };
            return userContactViewModel;
        }
        [HttpPut]
        
        public IActionResult PutContactFirst(ContactId contact)
        {
            Guid userId = Guid.Parse(User.Claims.First(o => o.Type == "UserId").Value);
            try
            {
                userContactsServices.PutContactFirst(userId, contact.User2Id);
                return Ok("Contact set first");
            }
            catch(Exception e)
            {
                return BadRequest("A problem occurred");
            }
        }
    }
}
