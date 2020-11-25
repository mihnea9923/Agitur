using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agitur.APIModel.Groups
{
    public class GroupViewModel
    {
        public string Name { get; set; }
        public string Photo { get; set; }
        public GroupViewModel(string name , string photo)
        {
            Name = name;
            Photo = photo;
        }
    }
}
