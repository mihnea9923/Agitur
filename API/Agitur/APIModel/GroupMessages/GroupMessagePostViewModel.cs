using Agitur.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agitur.APIModel.GroupMessages
{
    public class GroupMessagePostViewModel
    {
        public string Text { get; set; }
        public Guid GroupId { get; set; }
    }
}
