using System;
using System.Collections.Generic;
using System.Text;

namespace Agitur.Model
{
    public class GroupMessage
    {
        public Guid Id { get; set; }
        public virtual User Sender { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public virtual Group Group { get; set; }
        public virtual List<GroupMessageRead> MessageRead { get; set; }
        public GroupMessage()
        {
            MessageRead = new List<GroupMessageRead>();
        }
        public void SetMessageReaders(List<User> users , Guid userId)
        {
            foreach(var user in users)
            {
                var messageRead = new GroupMessageRead() { User = user };
                if (user.Id == userId)
                {
                    messageRead.Read = true;
                }
                MessageRead.Add(messageRead);
            }
        }
        public void UserReadTheMessage(Guid userId)
        {
            foreach(var iterator in MessageRead)
            {
                if(iterator.User.Id == userId)
                {
                    iterator.Read = true;
                    break;
                }
            }
        }
    }
}
