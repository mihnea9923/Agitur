using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Agitur.APIModel.Vocal;
using Agitur.ApplicationLogic;
using Agitur.Model;
using Agitur.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Agitur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VocalMessageController : ControllerBase
    {
        private readonly UserServices userServices;
        private readonly VocalMessageServices vocalMessageServices;
        private readonly UserContactsServices userContactsServices;
        private readonly IHubContext<ChatHub> hubContext;

        public VocalMessageController(UserServices userServices, VocalMessageServices vocalMessageServices,
            UserContactsServices userContactsServices, IHubContext<ChatHub> hubContext)
        {
            this.userServices = userServices;
            this.vocalMessageServices = vocalMessageServices;
            this.userContactsServices = userContactsServices;
            this.hubContext = hubContext;
        }
        [HttpPost]
        [Route("{recipientId}")]
        public async Task<IActionResult> Create([FromForm] IFormFile audioFile, Guid recipientId)
        {
            Guid senderId = Guid.Parse(User.Claims.First(o => o.Type == "UserId").Value);
            try
            {
                VocalMessage vocalMessage = new VocalMessage()
                {
                    Date = DateTime.Now,
                    RecipientId = recipientId,
                    SenderId = senderId
                };
                vocalMessageServices.TransformToByte(audioFile, vocalMessage);
                vocalMessageServices.Add(vocalMessage);
                userContactsServices.PutContactFirst(senderId, recipientId);
                userContactsServices.PutContactFirst(recipientId, senderId);
                var model = new VocalMessageGetViewModel
                    (vocalMessage.Id, vocalMessage.Date, senderId, recipientId);
                await hubContext.Clients.All.SendAsync("updateContactUponReceivingVocal");
                await hubContext.Clients.All.SendAsync("newVocalMessage", model);
                return Ok("Vocal message created");
            }
            catch (Exception e)
            {
                return BadRequest("Failed to create vocal message " + e.Message);
            }
        }
        [HttpGet]
        [Route("{recipientId}")]
        public IEnumerable<VocalMessageGetViewModel> Get(Guid recipientId)
        {
            List<VocalMessageGetViewModel> model = new List<VocalMessageGetViewModel>();
            Guid senderId = Guid.Parse(User.Claims.First(o => o.Type == "UserId").Value);
            var vocalMessages = vocalMessageServices.GetAll(senderId, recipientId);
            if (vocalMessages != null)
                foreach (var iterator in vocalMessages)
                {
                    model.Add(new VocalMessageGetViewModel(iterator.Id, iterator.Date,
                        iterator.SenderId, iterator.RecipientId));
                }

            return model;
        }
        [HttpGet]
        [Route("urlSource/{vocalId}")]
        public IActionResult GetUrlSource(Guid vocalId)
        {
            VocalMessage message = vocalMessageServices.GetById(vocalId);
            return File(VocalMessageGetViewModel.ConvertToMemoryStream(message.UrlSource), "audio/wav", "audioFile");
        }
    }
}
