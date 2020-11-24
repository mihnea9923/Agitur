using Agitur.DataAccess.Abstractions;
using Agitur.Model;
using System;
using System.Collections.Generic;
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
    }
}
