using Agitur.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agitur.DataAccess.Abstractions
{
    public interface IUserGroupRepository
    {
        void Add(UserGroup userGroup);
        void Delete(UserGroup userGroup);
        void Update(UserGroup userGroup);

        IEnumerable<Group> GetUserGroups(Guid userId);
        Dictionary<Group, List<User>> GetGroupMembersForUser(Guid userId);
        IEnumerable<User> GetGroupMembers(Group group);
        IEnumerable<UserGroup> GetUserGroupsInformations(Guid userId);
        IEnumerable<User> GetGroupMembers(Guid groupId);
        void SaveChanges();
        void RemoveUserFromGroup(User user, Group group);
    }
}
