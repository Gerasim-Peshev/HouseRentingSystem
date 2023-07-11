using System.ComponentModel.DataAnnotations;
using HouseRentingSystem.Services.House.Models;
using static HouseRentingSystem.Data.DataConstants.House;

namespace HouseRentingSystem.Models.House
{
    public class AddHouseViewModel
    {
        public AddHouseViewModel()
        {
            this.Categories = new List<HouseCategoryServiceModel>();
        }

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
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Required]
        [Range(PricePerMonthMinValue, PricePerMonthMaxValue, ErrorMessage = "Price Per Month must be a positive number and less than {2} leva.")]
        [Display(Name = "Price Per Month")]
        public decimal PricePerMonth { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public IEnumerable<HouseCategoryServiceModel> Categories { get; set; }
    }
}
