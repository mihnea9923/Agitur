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
        Dictionary<Group, List<User>> GetGroupMembers(Guid userId);
        IEnumerable<User> GetGroupMembers(Group group);
    }
}
