using System;
using System.Collections.Generic;
using System.Text;

namespace Agitur.Model
{
    public class VocalMessage
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid SenderId { get; set; }
        public Guid RecipientId { get; set; }
        public byte[] UrlSource { get; set; }
        //public bool Read { get; set; }
        public VocalMessage()
        {
            //Read = false;
        }
       
    }
}
