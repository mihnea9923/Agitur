using Agitur.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agitur.DataAccess.Abstractions
{
    public interface IGroupMessageRepository
    {
        void Add(GroupMessage message);
        void Update(GroupMessage message);
        GroupMessage GetLastMessage(Guid groupId);
        IEnumerable<GroupMessage> GetGroupMessages(Guid groupId);
    }
}
