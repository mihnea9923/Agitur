using Agitur.Model;
using System;
using System.Collections.Generic;

namespace Agitur.DataAccess.Abstractions
{
    public interface IMessageRepository
    {
        void Add(Message message);
        Message GetById(Guid id);
        void Update(Message message);

        //returns the messages between 2 users
        IEnumerable<Message> GetUserMessages(Guid senderId , Guid recipientId);
        Message FindLastMessage(Guid senderId , Guid recipientId);
    }
}
