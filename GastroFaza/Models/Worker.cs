namespace GastroFaza.Models
{
    public class Worker : User
    {
        public float Salary { get; set; }
        public int Rating { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; } = null!;
    }
}
