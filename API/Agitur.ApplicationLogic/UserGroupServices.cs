using Agitur.DataAccess.Abstractions;
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
    }
}
