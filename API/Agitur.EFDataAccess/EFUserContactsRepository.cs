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
            return context.UserContacts.Where(o => o.User1.Id == userId).OrderBy(o => o.Position);
        }

        public void Update(UserContacts userContact)
        {
            context.Update(userContact);
            context.SaveChanges();
        }
        public UserContacts GetUserContact(User user1, User user2)
        {
            return context.UserContacts.Where(o => o.User1 == user1 && o.User2 == user2).FirstOrDefault();
        }


        void IUserContactsRepository.SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
