using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Agitur.APIModel.Vocal
{
    public class VocalMessageGetViewModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid SenderId { get; set; }
        public Guid RecipientId { get; set; }
        //public MemoryStream UrlSource { get; set; }
        public VocalMessageGetViewModel(Guid id , DateTime date , Guid senderId , Guid recipientId)
        {
            Id = id;
            Date = date;
            SenderId = senderId;
            RecipientId = recipientId;
        }
        public static MemoryStream ConvertToMemoryStream(byte[] urlSource)
        {
            MemoryStream UrlSource = new MemoryStream();
            UrlSource.Write(urlSource, 0, urlSource.Length);
            UrlSource.Position = 0;
            return UrlSource;

        }
    }
}
