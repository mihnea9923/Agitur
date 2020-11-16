using Agitur.DataAccess.Abstractions;
using Agitur.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agitur.EFDataAccess
{
    public class EFUserContactsRepository : IUserContactsRepository
     {
        private readonly AgiturDbContext context;

        public EFUserContactsRepository(AgiturDbContext context)
        {
            this.context = context;
        }

        public void Add(UserContacts userContacts)
        {
            context.UserContacts.Add(userContacts);
            context.SaveChanges();
            //User user = new User();
            //user = userContacts.User1;
            //userContacts.User1 = userContacts.User2;
            //userContacts.User1 = user;
            UserContacts userContacts2 = new UserContacts()
            {
                Position = userContacts.User2.ContactsNumber,
                User1 = userContacts.User2,
                User2 = userContacts.User1
            };
            context.UserContacts.Add(userContacts2);
            context.SaveChanges();
        }

        public IEnumerable<UserContacts> GetUserContacts(Guid userId)
        {
            return context.UserContacts.Where(o => o.User1.Id == userId);
        }
    }
}
