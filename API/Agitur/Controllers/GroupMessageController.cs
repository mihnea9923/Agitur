using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agitur.APIModel.GroupMessages;
using Agitur.ApplicationLogic;
using Agitur.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Agitur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupMessageController : ControllerBase
    {
        private readonly UserServices userServices;
        private readonly GroupServices groupServices;
        private readonly UserGroupServices userGroupServices;
        private readonly GroupMessageServices groupMessageServices;

        public GroupMessageController(UserServices userServices, GroupServices groupServices,
            UserGroupServices userGroupServices, GroupMessageServices groupMessageServices)
        {
            this.userServices = userServices;
            this.groupServices = groupServices;
            this.userGroupServices = userGroupServices;
            this.groupMessageServices = groupMessageServices;
        }

        [HttpGet]
        [Route("{groupId}")]
        public IEnumerable<GroupMessagesGetViewModel> GetGroupMessages(Guid groupId)
        {
            IEnumerable<GroupMessage> messages = groupMessageServices.GetSortedGroupMessages(groupId);
            List<GroupMessagesGetViewModel> result = new List<GroupMessagesGetViewModel>();
            foreach (var message in messages)
            {
                GroupMessagesGetViewModel temp = new GroupMessagesGetViewModel(message.Id , message.Text , message.Time , message.Sender.Id);
                result.Add(temp);
            }
            return result;
        }
        [HttpPost]
        public IActionResult Create(GroupMessagePostViewModel model)
        {
            Guid userId = Guid.Parse(User.Claims.First(o => o.Type == "UserId").Value);
            try
            {


                GroupMessage message = new GroupMessage
                {
                    Group = groupServices.GetById(model.GroupId),
                    Sender = userServices.GetById(userId),
                    Text = model.Text,
                    Time = DateTime.Now
                };
                groupMessageServices.Add(message);
                groupServices.PutGroupFirst(message.Group.Id);
                return Ok("Message created");
            }
            catch (Exception e)
            {
                return BadRequest("Message Not created");
            }
        }

    }
}
