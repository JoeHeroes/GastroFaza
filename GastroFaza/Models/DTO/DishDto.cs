using System.ComponentModel.DataAnnotations;

namespace GastroFaza.Models.DTO
{
	public class DishDto
	{
		[Required]
		[MaxLength(25)]
		public string Name { get; set; } = null!;
		public string Description { get; set; } = null!;
		public double Price { get; set; }
		public DishType DishType { get; set; }
	}
}
