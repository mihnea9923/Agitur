using System;
using System.Collections.Generic;
using System.Text;

namespace Agitur.Model
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Photo { get; set; }
        public virtual List<GroupMessage> GroupMessages { get; set; }
        public Group()
        {
            GroupMessages = new List<GroupMessage>();
        }
        public string ConvertPhotoToBase64()
        {
            if (Photo != null)
            {
                string imageBase64 = Convert.ToBase64String(Photo);
                string imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64);
                return imageDataURL;
            }
            return null;
        }

    }
}
