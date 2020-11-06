using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Infra.Data.Entity
{
   public class User:BaseEntityWithDate
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string EMail { get; set; }
        public string Phone { get; set; }
        public byte[] PasswordHash { get; set; }
        public int LoginFailed { get; set; }
        public DateTime LastLoginDate { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Explanation { get; set; }
        public bool MailPermission { get; set; }
        public bool MailConfirmation { get; set; } = false;
        public string MailConfirmationCode { get; set; } = Guid.NewGuid().ToString().Replace("-", "").ToLower();
        public bool IsChangePassword { get; set; } = false;
        public string ChangePasswordCode { get; set; } = Guid.NewGuid().ToString().Replace("-", "").ToLower();



    }
}
