using Agitur.DataAccess.Abstractions;
using Agitur.Model;
using System;

namespace Agitur.ApplicationLogic
{
    public class UserServices
    {
        private readonly IUserRepository userRepository;

        public UserServices(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public void CreateUser(User user)
        {
            userRepository.Add(user);
        }
        public User GetByUserId(string userId)
        {
            return userRepository.GetByUserId(userId);
        }
    }
}
