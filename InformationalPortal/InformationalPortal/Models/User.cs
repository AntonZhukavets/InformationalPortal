namespace InformationalPortal.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }        
        public string LastName { get; set; }        
        public string Email { get; set; }       
        public string Login { get; set; }        
        public string Password { get; set; }
        public bool IsBlocked { get; set; }
        public string RoleName { get; set; }
        public int RoleId { get; set; }

    }
}