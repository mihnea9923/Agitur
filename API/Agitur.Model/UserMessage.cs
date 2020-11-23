using Agitur.Identity;
using System;

namespace Agitur.Model
{
    public class UserMessage
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public Guid RecipientId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public bool Read { get; set; }
    }
}
