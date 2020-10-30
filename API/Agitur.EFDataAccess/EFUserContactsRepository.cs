using Agitur.DataAccess.Abstractions;
using Agitur.Model;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public IEnumerable<UserContacts> GetUserContacts(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
