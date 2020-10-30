using System;
using System.Collections.Generic;
using System.Text;

namespace Agitur.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public string AgiturUser { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        
    }
}
