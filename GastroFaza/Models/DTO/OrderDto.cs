using GastroFaza.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace GastroFaza.Models.DTO
{
    public class OrderDto
    {
        [Required]
        public Status Status { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int AddedById { get; set; }
    }
}
