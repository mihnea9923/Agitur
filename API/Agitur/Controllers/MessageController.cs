using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agitur.APIModel.Users;
using Agitur.ApplicationLogic;
using Agitur.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        public MessageController(MessageServices messageServices, UserServices userServices , 
            UserContactsServices userContactsServices)
        {
            this.messageServices = messageServices;
            this.userServices = userServices;
            this.userContactsServices = userContactsServices;
        }
        [HttpPost]

        public IActionResult Create(Message message)
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;

            try
            {
                message.Date = DateTime.Now;
                message.SenderId = Guid.Parse(userId);
                message.Read = false;
                messageServices.Create(message);
                userContactsServices.PutContactFirst(Guid.Parse(userId), message.RecipientId);
                userContactsServices.PutContactFirst(message.RecipientId, Guid.Parse(userId));
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
