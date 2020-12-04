using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Agitur.APIModel.Vocal;
using Agitur.ApplicationLogic;
using Agitur.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Agitur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VocalMessageController : ControllerBase
    {
        private readonly UserServices userServices;
        private readonly VocalMessageServices vocalMessageServices;

        public VocalMessageController(UserServices userServices , VocalMessageServices vocalMessageServices)
        {
            this.userServices = userServices;
            this.vocalMessageServices = vocalMessageServices;
        }
        [HttpPost]
        [Route("{recipientId}")]
        public IActionResult Create([FromForm]IFormFile audioFile , Guid recipientId)
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
                return Ok("Vocal message created");
            }
            catch(Exception e)
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
            if(vocalMessages != null)
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
