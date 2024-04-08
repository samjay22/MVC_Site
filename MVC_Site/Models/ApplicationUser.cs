using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MVC_Site.Models
{
    public class ApplicationUser : IdentityUser
    {

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string DisplayName { get; set; }
        public string Biography { get; set; } = string.Empty;
        public bool Enabled { get; set; }
        [Required]
        public  DateTime DateOfBirth { get; set; }

        public ICollection<UserPost> Posts { get; set; }

    }
}
