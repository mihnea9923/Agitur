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
    }
}
