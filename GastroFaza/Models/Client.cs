using System.ComponentModel.DataAnnotations;

namespace GastroFaza.Models
{
    public class Client : User
    {
        public int PhoneNumber { get; set; }
        public int OrderID { get; set; }
        //test
    }
}
