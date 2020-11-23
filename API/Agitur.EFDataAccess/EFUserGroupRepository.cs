using Agitur.DataAccess.Abstractions;
using Agitur.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agitur.EFDataAccess
{
    public class EFUserGroupRepository : IUserGroupRepository
    {
        private readonly AgiturDbContext context;

        public EFUserGroupRepository(AgiturDbContext context)
        {
            this.context = context;
        }

        public void Add(UserGroup userGroup)
        {
            context.UserGroups.Add(userGroup);
            context.SaveChanges();
        }

        public void Delete(UserGroup userGroup)
        {
            context.UserGroups.Remove(userGroup);
            context.SaveChanges();
        }

        public Dictionary<Group, List<User>> GetGroupMembers(Guid userId)
        {
            IEnumerable<Group> groups = GetUserGroups(userId);
            Dictionary<Group, List<User>> userGroupsMembers = new Dictionary<Group, List<User>>();
            foreach(var group in groups)
            {
                List<User> groupUsers = (List<User>)GetGroupMembers(group);
                userGroupsMembers.Add(group, groupUsers);
            }
            return userGroupsMembers;
        }

        public IEnumerable<User> GetGroupMembers(Group group)
        {
            return context.UserGroups.Where(o => o.Group == group).Select(o => o.User);
        }

        public IEnumerable<Group> GetUserGroups(Guid userId)
        {
            return context.UserGroups.Where(o => o.User.Id == userId).Select(o => o.Group);
        }

        public void Update(UserGroup userGroup)
        {
            context.UserGroups.Update(userGroup);
            context.SaveChanges();
        }
    }
}
