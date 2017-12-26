using System.ComponentModel.DataAnnotations;


namespace InformationalPortal.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage="First name is required")]
        [Display(Name="First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage="Incorrect format of email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Login is required")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        
    }
}