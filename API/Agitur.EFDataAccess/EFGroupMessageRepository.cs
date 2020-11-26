using Agitur.DataAccess.Abstractions;
using Agitur.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agitur.EFDataAccess
{
    public class EFGroupMessageRepository : IGroupMessageRepository
    {
        private readonly AgiturDbContext context;

        public EFGroupMessageRepository(AgiturDbContext context)
        {
            this.context = context;
        }

        public void Add(GroupMessage message)
        {
            context.GroupMessages.Add(message);
            context.SaveChanges();
        }

        public IEnumerable<GroupMessage> GetGroupMessages(Guid groupId)
        {
            return context.GroupMessages.Where(o => o.Id == groupId);
        }

        public GroupMessage GetLastMessage(Guid groupId)
        {
            var messages = context.GroupMessages.Where(o => o.Group.Id == groupId).AsEnumerable();
            return messages.Aggregate((agg, next) => next.Time > agg.Time ? next : agg);
        }

        public void Update(GroupMessage message)
        {
            context.GroupMessages.Update(message);
            context.SaveChanges();
        }
    }
}
