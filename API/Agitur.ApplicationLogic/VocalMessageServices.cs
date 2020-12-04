using Agitur.DataAccess.Abstractions;
using Agitur.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Agitur.ApplicationLogic
{
    public class VocalMessageServices
    {
        private readonly IVocalMessageRepository vocalMessageRepository;

        public VocalMessageServices(IVocalMessageRepository vocalMessageRepository)
        {
            this.vocalMessageRepository = vocalMessageRepository;
        }
        public void TransformToByte(IFormFile file , VocalMessage vocalMessage)
        {
            MemoryStream memoryStream = new MemoryStream();
            //copy the photo into the memory stream
            file.CopyTo(memoryStream);
            vocalMessage.UrlSource  = memoryStream.ToArray();
        }

        public void Add(VocalMessage vocalMessage)
        {
            vocalMessageRepository.Add(vocalMessage);
        }

        public IEnumerable<VocalMessage> GetAll(Guid senderId, Guid recipientId)
        {
            return vocalMessageRepository.GetAll(senderId, recipientId);
        }

        public VocalMessage GetById(Guid vocalId)
        {
            return vocalMessageRepository.GetById(vocalId);
        }
    }
}
