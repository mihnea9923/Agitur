using Agitur.DataAccess.Abstractions;
using Agitur.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agitur.EFDataAccess
{
    public class EFGroupRepository : IGroupRepository
    {
        private readonly AgiturDbContext context;

        public EFGroupRepository(AgiturDbContext context)
        {
            this.context = context;
        }
        public void Add(Group group)
        {
            context.Groups.Add(group);
            context.SaveChanges();
        }

        public void Delete(Group group)
        {
            context.Groups.Remove(group);
            context.SaveChanges();
        }

        public Group GetById(Guid id)
        {
            return context.Groups.Where(o => o.Id == id).FirstOrDefault();
        }

        public void Update(Group group)
        {
            context.Groups.Update(group);
            context.SaveChanges();
        }

    }
}
