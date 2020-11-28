using Agitur.DataAccess.Abstractions;
using Agitur.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Agitur.ApplicationLogic
{
    public class GroupServices
    {
        private readonly IGroupRepository groupRepository;
        private readonly IUserGroupRepository userGroupRepository;
        private readonly UserGroupServices userGroupServices;

        public GroupServices(IGroupRepository groupRepository, IUserGroupRepository userGroupRepository,
            UserGroupServices userGroupServices)
        {
            this.groupRepository = groupRepository;
            this.userGroupRepository = userGroupRepository;
            this.userGroupServices = userGroupServices;
        }
        public Group Add(string name)
        {
            Group group = new Group() { Name = name, Id = Guid.NewGuid() };
            groupRepository.Add(group);
            return group;
        }

        public void AddMemberToGroup(User user, Group group)
        {
            UserGroup userGroup = new UserGroup()
            {
                Group = group,
                User = user,
                Role = Role.Member,
                Position = user.GroupsNumber
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

        public void PutGroupFirst(Guid groupId)
        {
            IEnumerable<User> users = userGroupRepository.GetGroupMembers(groupId).ToList();
            foreach (var user in users)
            {
                int position = 0;

                List<UserGroup> userGroups = userGroupRepository.GetUserGroupsInformations(user.Id).ToList();
                for (int i = 0; i < userGroups.Count(); i++)
                {
                    if (userGroups[i].Group.Id == groupId)
                    {
                        position = userGroups[i].Position;
                        userGroups[i].Position = 0;
                        userGroupServices.Update(userGroups[i]);
                        break;
                    }
                }
                foreach (var iterator in userGroups)
                {
                    if (iterator.Position < position)
                    {
                        iterator.Position++;
                        userGroupServices.Update(iterator);
                    }
                }
                //userGroupRepository.SaveChanges();
            }
        }
    }
}
