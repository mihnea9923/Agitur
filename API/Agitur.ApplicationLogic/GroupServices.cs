using Agitur.DataAccess.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agitur.ApplicationLogic
{
    public class GroupServices
    {
        private readonly IGroupRepository groupRepository;

        public GroupServices(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }
    }
}
