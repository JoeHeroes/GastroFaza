using GastroFaza.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace GastroFaza.Models.DTO
{
	public class DishDto
	{

        public int Id { get; set; }
        [Required]
		[MaxLength(25)]
		public string Name { get; set; } 
		public string Description { get; set; } 
		public double Price { get; set; }
		public DishType DishType { get; set; }
        public IFormFile PathPic { get; set; }
    }
}
