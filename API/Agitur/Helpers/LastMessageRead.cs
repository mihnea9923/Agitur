using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agitur.Helpers
{
    public class LastMessageRead
    {
        public Guid UserId { get; set; }
        public bool Read { get; set; }
        public LastMessageRead(Guid userId , bool read)
        {
            UserId = userId;
            Read = read;
        }
    }
}
