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

        public Dictionary<Group, List<User>> GetGroupMembersForUser(Guid userId)
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
            return context.UserGroups.Where(o => o.User.Id == userId).OrderBy(o => o.Position).Select(o => o.Group);
        }

        public IEnumerable<UserGroup> GetUserGroupsInformations(Guid userId)
        {
            return context.UserGroups.Where(o => o.User.Id == userId);
        }

        public void Update(UserGroup userGroup)
        {
            context.UserGroups.Update(userGroup);
            context.SaveChanges();
        }
        

        void IUserGroupRepository.SaveChanges()
        {
            context.SaveChanges();
        }

        public IEnumerable<User> GetGroupMembers(Guid groupId)
        {
            return context.UserGroups.Where(o => o.Group.Id == groupId).Select(o => o.User);
        }
        public void RemoveUserFromGroup(User user , Group group)
        {
            UserGroup userGroup = context.UserGroups.Where(o => o.User == user && o.Group.Id == group.Id).
                FirstOrDefault();
            context.UserGroups.Remove(userGroup);
            context.SaveChanges();
        }
    }
}
