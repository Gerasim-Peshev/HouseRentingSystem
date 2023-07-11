using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using static HouseRentingSystem.Data.DataConstants.Agent;

namespace HouseRentingSystem.Data.Models
{
    public class Agent
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        public string PhoneNumber { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
