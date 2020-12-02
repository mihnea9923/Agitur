using Agitur.Helpers;
using Agitur.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agitur.APIModel.Groups
{
    public class GroupViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public string LastMessage { get; set; }
        public DateTime LastMessageTime { get; set; }
        public List<LastMessageRead> LastMessageRead { get; set; }
        public GroupViewModel(string name , string photo , string lastMessage , DateTime lastMessageTime , 
            Guid id , List<LastMessageRead> lastMessageRead)
        {
            Name = name;
            Photo = photo;
            LastMessage = lastMessage;
            LastMessageTime = lastMessageTime;
            Id = id;
            LastMessageRead = lastMessageRead;
        }
        public static void LastGroupMessageRead(List<GroupMessageRead> messageRead, List<LastMessageRead> lastMessageRead)
        {
            foreach (var iterator in messageRead)
            {
                lastMessageRead.Add(new LastMessageRead(iterator.User.Id, iterator.Read));
            }
        }
    }
}
