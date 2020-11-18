using Agitur.DataAccess.Abstractions;
using Agitur.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agitur.EFDataAccess
{
    public class EFMessageRepository : IMessageRepository
    {
        private readonly AgiturDbContext context;

        public EFMessageRepository(AgiturDbContext context)
        {
            this.context = context;
        }
        public void Add(Message message)
        {
            context.Messages.Add(message);
            context.SaveChanges();
        }

        public Message FindLastMessage(Guid senderId, Guid recipientId)
        {

           var messages = context.Messages.Where(o => (o.SenderId == senderId && o.RecipientId == recipientId) ||
            (o.SenderId == recipientId && o.RecipientId == senderId)).AsEnumerable();
           return messages.Aggregate((agg, next) => next.Date > agg.Date ? next : agg);
        }

        public Message GetById(Guid id)
        {
            return context.Messages.Where(o => o.Id == id).FirstOrDefault();
        }

        public IEnumerable<Message> GetUserMessages(Guid userId , Guid recipientId)
        {
            return context.Messages.Where(o => o.SenderId == userId && o.RecipientId == recipientId
            || (o.SenderId == recipientId && o.RecipientId == userId));
        }

        public void Update(Message message)
        {
            context.Update(message);
            context.SaveChanges();
        }
    }
}
