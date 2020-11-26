using Agitur.DataAccess.Abstractions;
using Agitur.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agitur.ApplicationLogic
{
    public class GroupMessageServices
    {
        private readonly IGroupMessageRepository groupMessageRepository;

        public GroupMessageServices(IGroupMessageRepository groupMessageRepository)
        {
            this.groupMessageRepository = groupMessageRepository;
        }
        public void Add(GroupMessage message)
        {
            groupMessageRepository.Add(message);
        }
        public void Update(GroupMessage message)
        {
            groupMessageRepository.Update(message);
        }
        public GroupMessage GetLastMessage(Guid groupId)
        {
            return groupMessageRepository.GetLastMessage(groupId);
        }
        public IEnumerable<GroupMessage> GetSortedGroupMessages(Guid groupId)
        {
            return groupMessageRepository.GetGroupMessages(groupId).ToList().OrderBy(o => o.Time);
        }
    }
}
