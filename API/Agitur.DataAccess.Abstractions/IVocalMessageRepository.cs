using Agitur.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agitur.DataAccess.Abstractions
{
    public interface IVocalMessageRepository
    {
        void Add(VocalMessage vocalMessage);
        void Update(VocalMessage vocalMessage);
        void Delete(VocalMessage vocalMessage);
        VocalMessage GetById(Guid id);
        IEnumerable<VocalMessage> GetAll(Guid senderId, Guid recipientId);
        VocalMessage GetLastMessage(Guid senderId, Guid recipientId);

    }
}
