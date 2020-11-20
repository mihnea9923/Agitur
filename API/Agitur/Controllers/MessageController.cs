using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agitur.APIModel.Contacts;
using Agitur.APIModel.Users;
using Agitur.ApplicationLogic;
using Agitur.Model;
using Agitur.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Agitur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessageController : ControllerBase
    {
        private readonly MessageServices messageServices;
        private readonly UserServices userServices;
        private readonly UserContactsServices userContactsServices;
        private readonly IHubContext<ChatHub> chatHub;

        public MessageController(MessageServices messageServices, UserServices userServices , 
            UserContactsServices userContactsServices , IHubContext<ChatHub> chatHub)
        {
            this.messageServices = messageServices;
            this.userServices = userServices;
            this.userContactsServices = userContactsServices;
            this.chatHub = chatHub;
        }
        [HttpPost]

        public async Task<IActionResult> Create(Message message)
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;

            try
            {
                message.Date = DateTime.Now;
                message.SenderId = Guid.Parse(userId);
                message.Read = false;
                messageServices.Create(message);
                User user1 = userServices.GetById(Guid.Parse(userId));
                User user2 = userServices.GetById(message.RecipientId);
                bool contactExists = userContactsServices.Exists(Guid.Parse(userId), message.RecipientId);
                if (contactExists == false)
                {
                    UserContacts userContacts = new UserContacts()
                    {
                        User1 = user1,
                        User2 = user2
                    };
                    userContacts.Position = userContacts.User1.ContactsNumber;
                    userContactsServices.AddContact(userContacts);
                }
                userContactsServices.PutContactFirst(Guid.Parse(userId), message.RecipientId);
                userContactsServices.PutContactFirst(message.RecipientId, Guid.Parse(userId));
                if (contactExists == false)
                {
                    UserContactViewModel userContactViewModel1 = new UserContactViewModel()
                    {
                        Id = user1.Id,
                        Message = message.Text,
                        MessageRead = false,
                        MessageTime = message.Date,
                        Name = user1.Name,
                        Position = user1.ContactsNumber - 1,
                        Received = true ,
                        ProfilePhoto = user1.ConvertPhotoToBase64()
                    };
                    UserContactViewModel userContactViewModel2 = new UserContactViewModel()
                    {
                        Id = user2.Id,
                        Message = message.Text,
                        MessageRead = true,
                        MessageTime = message.Date,
                        Name = user2.Name,
                        Position = user2.ContactsNumber - 1,
                        Received = false,
                        ProfilePhoto = user2.ConvertPhotoToBase64()
                    };
                    await chatHub.Clients.All.SendAsync("refreshContacts", userContactViewModel1, userContactViewModel2);
                }
                await chatHub.Clients.All.SendAsync("refreshMessages", message.RecipientId , message.SenderId , message.Text);
                return Ok("Message was created");
            }
            catch (Exception e)
            {
                return BadRequest("Message was not created");
            }
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                Message message = messageServices.GetById(id);
                return Ok(message);
            }
            catch (Exception e)
            {
                return BadRequest(new { errorMessage = "Failed to get message" });
            }
        }
        //this method should return the messages of the user issuing the request with the user whose id is 
        //passed/transmitted as action parameter
        [HttpGet]
        [Route("conversation/{interlocutorId}")]
        public IEnumerable<Message> GetMessages(Guid interlocutorId)
        {

            string userId = User.Claims.First(o => o.Type == "UserId").Value;
            List<Message> messages = messageServices.GetMessages(Guid.Parse(userId), interlocutorId).ToList();
            messages.OrderBy(o => o.Date);
            return messages;
               
        }
        //mark message as read
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateMessage(Guid id)
        {
            try
            {
                Message message = messageServices.GetById(id);
                message.Read = true;
                messageServices.Update(message);
                return Ok("Message marked as read");
            }
            catch (Exception e)
            {
                return BadRequest("Message can t be marked as read");
            }
        }
        [HttpGet]
        [Route("findInterlocutors/{email}")]
        public IEnumerable<UserWithPhotoViewModel> FindInterlocutors(string email)
        {
            IEnumerable<User> users = userServices.GetAllByEmail(email);
            List<UserWithPhotoViewModel> usersWithPhoto = new List<UserWithPhotoViewModel>();
            foreach(var user in users)
            {
                UserWithPhotoViewModel temp = new UserWithPhotoViewModel()
                {
                    user = user,
                    ProfilePhoto = user.ConvertPhotoToBase64()
                };
                usersWithPhoto.Add(temp);
            }
            return usersWithPhoto;
        }

    }
}
