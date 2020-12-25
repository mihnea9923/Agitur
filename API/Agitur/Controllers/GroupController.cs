using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agitur.APIModel.Contacts;
using Agitur.APIModel.Groups;
using Agitur.ApplicationLogic;
using Agitur.Helpers;
using Agitur.Model;
using Agitur.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Agitur.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly GroupServices groupServices;
        private readonly UserServices userServices;
        private readonly GroupMessageServices groupMessageServices;
        private readonly IHubContext<ChatHub> hub;
        private readonly UserGroupServices userGroupServices;

        public GroupController(GroupServices groupServices, UserServices userServices,
            GroupMessageServices groupMessageServices, IHubContext<ChatHub> hub, UserGroupServices userGroupServices)
        {
            this.groupServices = groupServices;
            this.userServices = userServices;
            this.groupMessageServices = groupMessageServices;
            this.hub = hub;
            this.userGroupServices = userGroupServices;
        }

        [HttpPost]
        [Route("{groupName}")]
        public IActionResult Create(IEnumerable<UserContactViewModel> model, string groupName)
        {
            Guid userId = Guid.Parse(User.Claims.First(o => o.Type == "UserId").Value);
            User requestOwner = userServices.GetById(userId);
            Group group = groupServices.Add(groupName);
            groupServices.AddMemberToGroup(requestOwner, group);
            requestOwner.IncreaaseGroupsNumber();
            userServices.Update(requestOwner);
            GroupMessage groupMessage = new GroupMessage()
            {
                Sender = requestOwner,
                Group = group,
                Text = "Group " + group.Name + " was created",
                Time = DateTime.Now
            };
            groupMessageServices.Add(groupMessage);
            foreach (var iterator in model)
            {
                User user = userServices.GetById(iterator.Id);
                groupServices.AddMemberToGroup(user, group);
                user.IncreaaseGroupsNumber();
                userServices.Update(user);
            }
            groupMessage.SetMessageReaders(userGroupServices.GetGroupUsers(group.Id).ToList(), userId);
            groupMessageServices.Update(groupMessage);
            groupServices.PutGroupFirst(group.Id);
            return Ok(group.Id);
        }
        [HttpPost]
        [Route("photo/{groupId}")]
        public async Task<IActionResult> SetPhoto(Guid groupId)
        {

            IEnumerable<Guid> groupUsersId = userGroupServices.GetGroupUsers(groupId).Select(o => o.Id);
            if (Request.Form.Files.Count > 0)
            {
                IFormFile photo = Request.Form.Files[0];
                groupServices.AddPhoto(groupId, photo);
            }
            await hub.Clients.All.SendAsync("groupCreated", groupUsersId);
            return Ok("Photo was added");
        }
        [HttpGet]
        public IEnumerable<GroupViewModel> Groups()
        {
            Guid userId = Guid.Parse(User.Claims.First(o => o.Type == "UserId").Value);
            User user = userServices.GetById(userId);
            IEnumerable<Group> groups = groupServices.GetGroups(user);
            List<GroupViewModel> groupsWithPhoto = new List<GroupViewModel>();
            if (groups != null)
            {
                foreach (var group in groups)
                {
                    GroupMessage lastMessage = groupMessageServices.GetLastMessage(group.Id);
                    List<LastMessageRead> lastMessageRead = new List<LastMessageRead>();
                    GroupViewModel.LastGroupMessageRead(lastMessage.MessageRead, lastMessageRead);
                    GroupViewModel temp = new GroupViewModel(group.Name, group.ConvertPhotoToBase64(), lastMessage.Text, lastMessage.Time, group.Id, lastMessageRead);
                    groupsWithPhoto.Add(temp);
                }
            }
            return groupsWithPhoto;
        }
        [HttpPut]
        [Route("leaveGroup/{groupId}")]
        public IActionResult LeaveGroup(Guid groupId)
        {
            Guid userId = Guid.Parse(User.Claims.First(o => o.Type == "UserId").Value);
            try
            {
                User user = userServices.GetById(userId);
                Group group = groupServices.GetById(groupId);
                userGroupServices.RemoveUserFromGroup(user, group);
                return Ok("User removed from group");
            }
            catch (Exception e)
            {
                return BadRequest("Failed to leave group with id " + groupId);
            }
        }

    }
}
