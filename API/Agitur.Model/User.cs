using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Agitur.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public string AgiturUser { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        
        public int ContactsNumber { get; set; }
        public byte[] ProfilePhoto { get; set; }
        public User()
        {
            ContactsNumber = 0;
        }
        public void IncreaseContactsNumber()
        {
            ContactsNumber++;
        }

       
    }
}
