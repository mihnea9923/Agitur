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
        public void Create(Message message)
        {
            messageRepository.Add(message);
        }
        public void Update(Message message)
        {
            messageRepository.Update(message);
        }
        public Message GetById(Guid id)
        {
            return messageRepository.GetById(id);
        }
        public IEnumerable<Message> GetMessages(Guid senderId , Guid recipientId)
        {
            return messageRepository.GetUserMessages(senderId, recipientId);
        }
    }
}
