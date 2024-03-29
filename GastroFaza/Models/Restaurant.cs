﻿using System.ComponentModel.DataAnnotations;

namespace GastroFaza.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; }
        public bool HasDelivery { get; set; }
        [EmailAddress]
        public string ContactEmail { get; set; } 
        [PhoneAttribute]
        public string ContactNumber { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; } 
        public virtual List<Worker> Workers { get; set; } = null!;
        public virtual List<Client> Clients { get; set; } = null!;
        public virtual List<DiningTable> Tables { get; set; } = null!;
    }
}
