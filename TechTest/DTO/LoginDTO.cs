using System.ComponentModel.DataAnnotations;

namespace TechTest.DTO
{
    public class LoginDTO
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsValid() 
        {
            return !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password) &&
                UserName?.Length <=20;
        }
    }
}