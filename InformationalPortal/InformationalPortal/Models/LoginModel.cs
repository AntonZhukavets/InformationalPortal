﻿using System.ComponentModel.DataAnnotations;


namespace InformationalPortal.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Login is required")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}