using Agitur.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agitur.APIModel.Users
{
    public class UserWithPhotoViewModel
    {
        public User user { get; set; }
        public string ProfilePhoto { get; set; }
    }
}
