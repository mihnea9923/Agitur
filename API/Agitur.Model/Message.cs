using Agitur.Identity;
using System;

namespace Agitur.Model
{
    public class Message
    {
        public Guid Id { get; set; }
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public bool Read { get; set; }
    }
}
