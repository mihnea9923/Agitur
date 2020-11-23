using Agitur.DataAccess.Abstractions;
using Agitur.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agitur.ApplicationLogic
{
    public class MessageServices
    {
        private readonly IMessageRepository messageRepository;

        public MessageServices(IMessageRepository messageRepository)
        {
            this.messageRepository = messageRepository;
        }
        public void Create(UserMessage message)
        {
            messageRepository.Add(message);
        }
        public void Update(UserMessage message)
        {
            messageRepository.Update(message);
        }
        public UserMessage GetById(Guid id)
        {
            return messageRepository.GetById(id);
        }
        public IEnumerable<UserMessage> GetMessages(Guid senderId , Guid recipientId)
        {
            return messageRepository.GetUserMessages(senderId, recipientId);
        }
        public UserMessage GetLastMessage(Guid senderId , Guid recipentId)
        {
            return messageRepository.FindLastMessage(senderId, recipentId);
        }
    }
}
