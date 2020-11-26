﻿using Agitur.DataAccess.Abstractions;
using Agitur.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Agitur.ApplicationLogic
{
    public class GroupServices
    {
        private readonly IGroupRepository groupRepository;
        private readonly IUserGroupRepository userGroupRepository;

        public GroupServices(IGroupRepository groupRepository , IUserGroupRepository userGroupRepository)
        {
            this.groupRepository = groupRepository;
            this.userGroupRepository = userGroupRepository;
        }
        public Group Add(string name)
        {
            Group group = new Group() { Name = name , Id = Guid.NewGuid() };
            groupRepository.Add(group);
            return group;
        }

        public void AddMemberToGroup(User user, Group group)
        {
            UserGroup userGroup = new UserGroup()
            {
                Group = group,
                User = user,
                Role = Role.Member
            };
            userGroupRepository.Add(userGroup);
        }

        public IEnumerable<Group> GetGroups(User user)
        {
            return userGroupRepository.GetUserGroups(user.Id);
        }

        public void AddPhoto(Guid groupId, IFormFile photo)
        {
            MemoryStream memoryStream = new MemoryStream();
            photo.CopyTo(memoryStream);
            Group group = GetById(groupId);
            group.Photo = memoryStream.ToArray();
            UpdateGroup(group);
        }

       

        private void UpdateGroup(Group group)
        {
            groupRepository.Update(group);
        }

        public Group GetById(Guid groupId)
        {
            return groupRepository.GetById(groupId);
        }
    }
}
