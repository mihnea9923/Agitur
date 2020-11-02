using Agitur.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agitur.DataAccess.Abstractions
{
    public interface IUserRepository
    {
        void Add(User user);
        User GetByUserId(string userId);
    }
}
