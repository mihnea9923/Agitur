using System;

namespace Agitur.Model
{
    public class GroupMessageRead
    {
        public Guid Id { get; set; }
        public virtual User User { get; set; }
        public bool Read { get; set; }
        public GroupMessageRead()
        {
            Read = false;
        }
    }
}