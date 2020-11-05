using System;
using System.Collections.Generic;
using System.Text;
using Core3Base.Infra.Data.Entity;

namespace Core3Base.Infra.CrossCutting.AdminIdentity.Entity
{
    public class User:BaseEntityWithDate
    {

        public string Name { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public DateTime? BirthDay { get; set; }
        //public string Password { get; set; }
        public string Phone { get; set; }
        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
        public string Explanation { get; set; }
        public string TCKN { get; set; }
        public bool MailPermission { get; set; }
        public bool MailConfirmation { get; set; } = false;
        public string MailConfirmationCode { get; set; } = Guid.NewGuid().ToString().Replace("-", "").ToLower();
        public bool IsChangePassword { get; set; } = false;
        public string ChangePasswordCode { get; set; } = Guid.NewGuid().ToString().Replace("-", "").ToLower();
    }
}
