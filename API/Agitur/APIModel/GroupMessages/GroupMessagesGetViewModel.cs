using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agitur.APIModel.GroupMessages
{
    public class GroupMessagesGetViewModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public Guid SenderId { get; set; }
        public string SenderPhoto { get; set; }
        public GroupMessagesGetViewModel(Guid id ,  string text , DateTime date , Guid senderId , string senderPhoto)
        {
            Id = id;
            Text = text;
            Date = date;
            SenderId = senderId;
            SenderPhoto = senderPhoto;
        }
    }
}
