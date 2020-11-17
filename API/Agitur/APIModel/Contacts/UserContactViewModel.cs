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
    }
}
