using Agitur.DataAccess.Abstractions;
using Agitur.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agitur.EFDataAccess
{
    public class EFVocalMessageRepository : IVocalMessageRepository
    {
        private readonly AgiturDbContext context;

        public EFVocalMessageRepository(AgiturDbContext context)
        {
            this.context = context;
        }
        public void Add(VocalMessage vocalMessage)
        {
            context.VocalMessages.Add(vocalMessage);
            context.SaveChanges();
        }

        public void Delete(VocalMessage vocalMessage)
        {
            context.VocalMessages.Remove(vocalMessage);
            context.SaveChanges();
        }

        public IEnumerable<VocalMessage> GetAll(Guid senderId, Guid recipientId)
        {
            return context.VocalMessages.Where(o => (o.SenderId == senderId && o.RecipientId == recipientId) || 
            (o.SenderId == recipientId && o.RecipientId == senderId)).
                OrderByDescending(o => o.Date).Take(10);
        }

        public VocalMessage GetById(Guid id)
        {
            return context.VocalMessages.Where(o => o.Id == id).FirstOrDefault();
        }

        public VocalMessage GetLastMessage(Guid senderId, Guid recipientId)
        {
            var messages = context.VocalMessages.Where(o => (o.SenderId == senderId && o.RecipientId == recipientId) ||
           (o.SenderId == recipientId && o.RecipientId == senderId)).AsEnumerable();
            if(messages != null && messages.Any())
            return messages.Aggregate((agg, next) => next.Date > agg.Date ? next : agg);
            return null;
        }

        public void Update(VocalMessage vocalMessage)
        {
            context.VocalMessages.Update(vocalMessage);
            context.SaveChanges();
        }
    }
}
