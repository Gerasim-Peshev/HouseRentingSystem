using System.ComponentModel.DataAnnotations;
using HouseRentingSystem.Data;
using static HouseRentingSystem.Data.DataConstants.Agent;

namespace HouseRentingSystem.Models.Agent
{
    public class BecomeAgentViewModel
    {
        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; } = null!;
    }
}
