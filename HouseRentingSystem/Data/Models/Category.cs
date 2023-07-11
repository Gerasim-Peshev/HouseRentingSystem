using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Data.DataConstants.Category;

namespace HouseRentingSystem.Data.Models
{
    public class Category
    {
        public Category()
        {
            this.Houses = new HashSet<House>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength)]
        public string Name { get; set; }
        public ICollection<House> Houses { get; set; }
    }
}
