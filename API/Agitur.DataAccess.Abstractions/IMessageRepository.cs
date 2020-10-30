using Agitur.Model;
using System;

namespace Agitur.DataAccess.Abstractions
{
    public interface IMessageRepository
    {
        void Add(Message message);
        Message GetById(Guid id);
        void Update(Message message);
    }
}
