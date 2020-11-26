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
        public string LastMessage { get; set; }
        public DateTime LastMessageTime { get; set; }
        public GroupViewModel(string name , string photo , string lastMessage , DateTime lastMessageTime)
        {
            Name = name;
            Photo = photo;
            LastMessage = lastMessage;
            LastMessageTime = lastMessageTime;
        }
    }
}
