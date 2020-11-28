using Agitur.DataAccess.Abstractions;
using Agitur.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agitur.ApplicationLogic
{
    public class UserGroupServices
    {
        private readonly IUserGroupRepository userGroupRepository;

        public UserGroupServices(IUserGroupRepository userGroupRepository)
        {
            this.userGroupRepository = userGroupRepository;
        }
        public void Update(UserGroup userGroup)
        {
            userGroupRepository.Update(userGroup);
        }

        public IEnumerable<User> GetGroupUsers(Guid id)
        {
            return userGroupRepository.GetGroupMembers(id);
        }
    }
}
