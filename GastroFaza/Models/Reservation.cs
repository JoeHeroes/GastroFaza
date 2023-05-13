using System.ComponentModel.DataAnnotations.Schema;

namespace GastroFaza.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int ClientId { get; set; }

        public string TableIdContainer { get; set; }
        [NotMapped]
        public int[] TableId
        {
            get
            {
                return Array.ConvertAll(TableIdContainer.Split(';'), Int32.Parse);
            }
            set
            {
                int[] _data = value;
                TableIdContainer = String.Join(";", _data.Select(p => p.ToString()).ToArray());
            }
        }
        public DateTime DataOfReservation{ get; set; }
    }
}
