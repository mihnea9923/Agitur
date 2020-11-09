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

        public IEnumerable<User> getAll()
        {
            return context.Users;
        }

        public User GetById(Guid userId)
        {
            return context.Users.Where(o => o.Id == userId).FirstOrDefault();
        }

        public User GetByUserId(string userId)
        {
            return context.Users.Where(o => o.AgiturUser == userId).FirstOrDefault();
        }

        public void Update(User user)
        {
            context.Users.Update(user);
            context.SaveChanges();
        }
    }
}
