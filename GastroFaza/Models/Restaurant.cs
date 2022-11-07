using System.ComponentModel.DataAnnotations;
using System.Net;

namespace GastroFaza.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool HasDelivery { get; set; }
        [EmailAddress]
        public string ContactEmail { get; set; } = null!;
        [PhoneAttribute]
        public string ContactNumber { get; set; } = null!;
        public int AddressID { get; set; }
        public virtual Address Address { get; set; } = null!;
        public virtual List<Dish> Menu { get; set; } = null!;
        public virtual List<Worker> Workers { get; set; } = null!;
        public virtual List<Client> Clients { get; set; } = null!;
        public virtual List<Tablee> Tables { get; set; } = null!;
    }
}
