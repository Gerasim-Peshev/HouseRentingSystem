using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static HouseRentingSystem.Data.DataConstants.House;

namespace HouseRentingSystem.Data.Models
{
    public class House
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        public string Address { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [Range(PricePerMonthMinValue, PricePerMonthMaxValue)]
        public decimal PricePerMonth { get; set; }

        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        [ForeignKey(nameof(Agent))]
        public int AgentId { get; set; }
        public Agent Agent { get; set; }

        public string? RenterId { get; set; }
    }
}
