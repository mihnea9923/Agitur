using Agitur.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agitur.DataAccess.Abstractions
{
    public interface IUserContactsRepository
    {
        void Add(UserContacts userContacts);
        IEnumerable<UserContacts> GetUserContacts(Guid userId);
        void Update(UserContacts userContact);
        UserContacts GetUserContact(User user1, User user2);
        void SaveChanges();
    }
}
