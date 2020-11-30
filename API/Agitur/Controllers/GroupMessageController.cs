using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agitur.APIModel.GroupMessages;
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
    public class GroupMessageController : ControllerBase
    {
        private readonly UserServices userServices;
        private readonly GroupServices groupServices;
        private readonly UserGroupServices userGroupServices;
        private readonly GroupMessageServices groupMessageServices;
        private readonly IHubContext<ChatHub> hub;

        public GroupMessageController(UserServices userServices, GroupServices groupServices,
            UserGroupServices userGroupServices, GroupMessageServices groupMessageServices , IHubContext<ChatHub> hub)
        {
            this.userServices = userServices;
            this.groupServices = groupServices;
            this.userGroupServices = userGroupServices;
            this.groupMessageServices = groupMessageServices;
            this.hub = hub;
        }

        [HttpGet]
        [Route("{groupId}")]
        public IEnumerable<GroupMessagesGetViewModel> GetGroupMessages(Guid groupId)
        {
            IEnumerable<GroupMessage> messages = groupMessageServices.GetSortedGroupMessages(groupId);
            List<GroupMessagesGetViewModel> result = new List<GroupMessagesGetViewModel>();
            foreach (var message in messages)
            {
                GroupMessagesGetViewModel temp = new GroupMessagesGetViewModel(message.Id , message.Text , 
                    message.Time , message.Sender.Id , message.Sender.ConvertPhotoToBase64());
                result.Add(temp);
            }
            return result;
        }
        [HttpPost]
        public async Task<IActionResult> Create(GroupMessagePostViewModel model)
        {
            Group group = groupServices.GetById(model.GroupId);
            Guid userId = Guid.Parse(User.Claims.First(o => o.Type == "UserId").Value);
            try
            {


                GroupMessage message = new GroupMessage
                {
                    Group = group,
                    Sender = userServices.GetById(userId),
                    Text = model.Text,
                    Time = DateTime.Now
                };
                groupMessageServices.Add(message);
                IEnumerable<Guid> groupUsersId = userGroupServices.GetGroupUsers(group.Id).Select(o => o.Id);
                await hub.Clients.All.SendAsync("putGroupFirst", group.Id , groupUsersId);
                await hub.Clients.All.SendAsync("groupMessage", message.Group.Id , message.Text , message.Time , groupUsersId);
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
