using Agitur.DataAccess.Abstractions;
using Agitur.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Agitur.ApplicationLogic
{
    public class UserServices
    {
        private readonly IUserRepository userRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        //inject IHttpContextAccessor so we can access the request and therefore the photo.No need for it anymore
        //but I will keep it here just in case I will need it later 
        public UserServices(IUserRepository userRepository , IHttpContextAccessor httpContextAccessor)
        {
            this.userRepository = userRepository;
            this.httpContextAccessor = httpContextAccessor;
        }
        public void CreateUser(User user)
        {
            userRepository.Add(user);
        }
        public User GetByUserId(string userId)
        {
            return userRepository.GetByUserId(userId);
        }
        public User GetById(Guid userId)
        {
            return userRepository.GetById(userId);
        }

        public void UpdateProfilePhoto(Guid userId , IFormFile photo)
        {
            User user = GetById(userId);
            MemoryStream memoryStream = new MemoryStream();
            //copy the photo into the memory stream
            photo.CopyTo(memoryStream);
            user.ProfilePhoto = memoryStream.ToArray();
            userRepository.Update(user);
        }

        public IEnumerable<User> GetAllByEmail(string email)
        {
            IEnumerable<User> users = userRepository.getAll();
            return users.Where(o => o.Email.ToLower().Contains(email.ToLower()));
        }
    }
}
