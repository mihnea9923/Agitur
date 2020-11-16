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
        private readonly IUserRepository userRepository;

        public UserContactsServices(IUserContactsRepository userContactsRepository , IUserRepository userRepository)
        {
            this.userContactsRepository = userContactsRepository;
            this.userRepository = userRepository;
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

        public void AddContact(UserContacts userContact)
        {
            userContactsRepository.Add(userContact);
            userContact.User1.ContactsNumber++;
            userContact.User2.ContactsNumber++;
            userRepository.Update(userContact.User1);
            userRepository.Update(userContact.User2);
        }
    }
}
