﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Agitur.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public string AgiturUser { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        
        public int ContactsNumber { get; set; }
        public byte[] ProfilePhoto { get; set; }
        public int GroupsNumber { get; set; }
        public User()
        {
            ContactsNumber = 0;
            GroupsNumber = 0;
        }
        public void IncreaseContactsNumber()
        {
            ContactsNumber++;
        }
        public void IncreaaseGroupsNumber()
        {
            GroupsNumber++;
        }
        public string ConvertPhotoToBase64()
        {
            if (ProfilePhoto != null)
            {
                string imageBase64 = Convert.ToBase64String(ProfilePhoto);
                string imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64);
                return imageDataURL;
            }
            return null;
        }

       
    }
}
