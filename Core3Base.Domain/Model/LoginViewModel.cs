using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Model
{
   public class LoginViewModel
   {    
        public string Email {get; set;}
        public string Password {get; set;}
        public bool HasError {get; set;}
        public string ErrorMessage {get; set;} 

   }
}        
