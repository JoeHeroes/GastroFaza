namespace GastroFaza.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string PostalCode { get; set; } = null!;


        public virtual Restaurant Restaurant { get; set; }
    }
}
