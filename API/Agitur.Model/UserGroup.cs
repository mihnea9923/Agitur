using System;
using System.Collections.Generic;
using System.Text;

namespace Agitur.Model
{
    public enum Role { Administrator , Member}
    public class UserGroup
    {
        public Guid Id { get; set; }
        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
        public int Position { get; set; }
        public Role Role { get; set; }
    }
}
