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
            IEnumerable<UserContacts> userContacts = userContactsRepository.GetUserContacts(userId);
            
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

        //when sending or receiving a message,the interlocutor must be put first in the list of contacts
        public void PutContactFirst(Guid user1Id , Guid user2Id)
        {
            User user1 = userRepository.GetById(user1Id);
            User user2 = userRepository.GetById(user2Id);
            UserContacts contact = GetUserContact(user1, user2);
            IEnumerable<UserContacts> userContacts = userContactsRepository.GetUserContacts(user1Id);
            
            foreach(var userContact in userContacts)
            {
                if (userContact.User1 == user1 && userContact.User2 == user2)
                {
                    userContact.Position = 0;
                    //update won't work because iterating on ienumerable returned by EF keeps connection to DB
                    //open and we can not open another one to update
                    //Update(userContact);
                }
                else if(userContact.Position < contact.Position)
                {
                    userContact.Position++;
                    //Update(userContact);
                }
            }
            //this works without updating because EF keeps track of objects
            userContactsRepository.SaveChanges();

        }

        public bool Exists(Guid user1, Guid user2)
        {
            return userContactsRepository.Exists(user1, user2);
        }

        private UserContacts GetUserContact(User user1, User user2)
        {
            return userContactsRepository.GetUserContact(user1, user2);
        }

        public void Update(UserContacts userContact)
        {
            userContactsRepository.Update(userContact);
        }

        public void RemoveContact(User requestOwner, User contact)
        {
            userContactsRepository.Remove(requestOwner, contact);
        }
    }
}
