using Agitur.DataAccess.Abstractions;
using Agitur.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agitur.EFDataAccess
{
    public class EFUserRepository : IUserRepository
    {
        private readonly AgiturDbContext context;

        public EFUserRepository(AgiturDbContext context)
        {
            this.context = context;
        }

        public void Add(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public User GetByUserId(string userId)
        {
            return context.Users.Where(o => o.AgiturUser == userId).FirstOrDefault();
        }
    }
}
