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

    }
}
