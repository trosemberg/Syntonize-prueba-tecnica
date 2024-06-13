namespace TechTest.DTO
{
    public class UsersDTO
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Role { get; set; }

        public bool IsValid() 
        {
            return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(UserName) &&
                !string.IsNullOrEmpty(FullName) && !string.IsNullOrEmpty(Password) &&
                UserName?.Length <=20;
        }
    }
}