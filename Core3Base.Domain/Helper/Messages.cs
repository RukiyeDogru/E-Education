using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Helper
{
   public static class Messages
    {

        public static string UserAdded = "Kullanıcı başarıyla eklendi";
        public static string UserDeleted = "Kullanıcı  başarıyla silindi";
        public static string UserUpdated = "Kullanıcı  başarıyla güncellendi";

        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string MailConfirmation = "Mail Onaylandı.";
        public static string MailConfirmationFailed = "Mail Onaylanamadı.";

        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Sisteme giriş başarılı";
        public static string SuccessfulLoginFailed = "Sisteme giriş Başarısız";
        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi";
        public static string AccessTokenCreated = "Access token başarıyla oluşturuldu";

        public static string AuthorizationDenied = "Yetkiniz yok";

        public static string LoginLimitFullMessage = "You logged in wrong 5 times. Your account is locked.";
        public static string LoginLimitErrorMessage = "You entered incorrectly.";
    }
}
