using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TeamFinder.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<SportEvent> Events { get; set; }
    }
}