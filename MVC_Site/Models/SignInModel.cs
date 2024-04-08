﻿using System.ComponentModel.DataAnnotations;

namespace MVC_Site.Models
{
    public class SignInModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
