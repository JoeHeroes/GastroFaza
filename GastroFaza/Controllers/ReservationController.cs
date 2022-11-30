using GastroFaza.Models;

namespace GastroFaza.Controllers
{
    public class ReservationController
    {
        private readonly RestaurantDbContext dbContext;

        public ReservationController(RestaurantDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
