using Agitur.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agitur.DataAccess.Abstractions
{
    public interface IGroupRepository
    {
        void Add(Group group);
        void Delete(Group group);
        Group GetById(Guid id);
        void Update(Group group);
    }
}
