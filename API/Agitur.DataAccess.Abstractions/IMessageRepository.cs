using Agitur.Model;
using System;
using System.Collections.Generic;

namespace Agitur.DataAccess.Abstractions
{
    public interface IMessageRepository
    {
        void Add(UserMessage message);
        UserMessage GetById(Guid id);
        void Update(UserMessage message);

        //returns the messages between 2 users
        IEnumerable<UserMessage> GetUserMessages(Guid senderId , Guid recipientId);
        UserMessage FindLastMessage(Guid senderId , Guid recipientId);
    }
}
