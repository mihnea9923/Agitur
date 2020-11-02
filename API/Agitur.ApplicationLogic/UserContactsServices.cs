using Agitur.DataAccess.Abstractions;
using Agitur.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agitur.ApplicationLogic
{
    public class UserContactsServices
    {
        private readonly IUserContactsRepository userContactsRepository;

        public UserContactsServices(IUserContactsRepository userContactsRepository)
        {
            this.userContactsRepository = userContactsRepository;
        }

        public IEnumerable<User> GetUserConctacts(Guid userId)
        {
            var userContacts = userContactsRepository.GetUserContacts(userId);
            List<User> users = new List<User>();
            if(userContacts != null)
            {
                foreach(var iterator in userContacts)
                {
                    users.Add(iterator.User2);
                }
            }
            return users;
            
        }
    }
}
