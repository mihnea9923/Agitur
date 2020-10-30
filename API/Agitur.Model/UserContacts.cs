using System;
using System.Collections.Generic;
using System.Text;

namespace Agitur.Model
{
    public class UserContacts
    {
        public Guid Id { get; set; }
        public virtual User User1 { get; set; }
        public virtual User User2 { get; set; }
        //represents the position of user2 in the contacts list of user1
        public int Position { get; set; }
         
    }
}
