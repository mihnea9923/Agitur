using Agitur.DataAccess.Abstractions;
using System;
using System.Collections.Generic;
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
    }
}
