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
            return context.VocalMessages.Where(o => o.SenderId == senderId && o.RecipientId == recipientId).
                OrderBy(o => o.Date).Take(10);
        }

        public VocalMessage GetById(Guid id)
        {
            return context.VocalMessages.Where(o => o.Id == id).FirstOrDefault();
        }

        public void Update(VocalMessage vocalMessage)
        {
            context.VocalMessages.Update(vocalMessage);
            context.SaveChanges();
        }
    }
}
