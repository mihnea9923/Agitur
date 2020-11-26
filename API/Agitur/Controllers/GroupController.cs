using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agitur.APIModel.Contacts;
using Agitur.APIModel.Groups;
using Agitur.ApplicationLogic;
using Agitur.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        public GroupController(GroupServices groupServices , UserServices userServices ,
            GroupMessageServices groupMessageServices)
        {
            this.groupServices = groupServices;
            this.userServices = userServices;
            this.groupMessageServices = groupMessageServices;
        }

        [HttpPost]
        [Route("{groupName}")]
        public IActionResult Create(IEnumerable<UserContactViewModel> model, string groupName )
        {
            Guid userId =  Guid.Parse(User.Claims.First(o => o.Type == "UserId").Value);
            //var photo = Request.Form.Files[0];
            User requestOwner = userServices.GetById(userId);
            Group group = groupServices.Add(groupName);
            groupServices.AddMemberToGroup(requestOwner, group);
            foreach (var iterator in model)
            {
                User user = userServices.GetById(iterator.Id);
                groupServices.AddMemberToGroup(user , group);
            }
            return Ok(group.Id);
        }
        [HttpPost]
        [Route("photo/{groupId}")]
        public IActionResult SetPhoto(Guid groupId)
        {
            IFormFile photo = Request.Form.Files[0];
            groupServices.AddPhoto(groupId, photo);
            return Ok("Photo was added");
        }
        [HttpGet]
        public IEnumerable<GroupViewModel> Groups()
        {
            Guid userId = Guid.Parse(User.Claims.First(o => o.Type == "UserId").Value);
            User user = userServices.GetById(userId);
            IEnumerable<Group> groups = groupServices.GetGroups(user);
            List<GroupViewModel> groupsWithPhoto = new List<GroupViewModel>();
            if(groups != null)
            {
                foreach(var group in groups)
                {
                    GroupMessage lastMessage = groupMessageServices.GetLastMessage(group.Id);
                    GroupViewModel temp = new GroupViewModel(group.Name, group.ConvertPhotoToBase64() , lastMessage.Text , lastMessage.Time);
                    groupsWithPhoto.Add(temp);
                }
            }
            return groupsWithPhoto;
        }
    }
}
