using Agitur.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agitur.APIModel.Contacts
{
    public class UserContactViewModel
    {
        public string ProfilePhoto { get; set; }
        public string Message { get; set; }
        public DateTime MessageTime { get; set; }
        public bool MessageRead { get; set; }
        public string Name { get; set; }
        public Guid Id { get; set; }
        public bool Received { get; set; }
        public int Position { get; set; }

        public static UserContactViewModel Create(UserMessage lastMessage, VocalMessage lastVocalMessage , Guid userId)
        {
            if (lastVocalMessage == null)
            {
                return new UserContactViewModel()
                {
                    Message = lastMessage.Text,
                    MessageRead = lastMessage.Read,
                    MessageTime = lastMessage.Date,
                    Received = lastMessage.SenderId != userId
                };
            }
            else return new UserContactViewModel()
            {
                Message = lastMessage.Date > lastVocalMessage.Date ? lastMessage.Text : "Vocal Message",
                MessageRead = lastMessage.Date > lastVocalMessage.Date ? lastMessage.Read : true,
                MessageTime = lastMessage.Date > lastVocalMessage.Date ? lastMessage.Date : lastVocalMessage.Date,
                Received = lastMessage.Date > lastVocalMessage.Date ? (lastMessage.SenderId != userId) : 
                (lastVocalMessage.SenderId != userId)
            };
        }
    }
}
