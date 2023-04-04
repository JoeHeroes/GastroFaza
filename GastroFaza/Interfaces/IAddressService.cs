using GastroFaza.Models;

namespace GastroFaza.Interfaces
{
    public interface IAddressService
    {
        Task<IEnumerable<Address>> GetAddress();
        Task<Address> GetAddres(int id);
    }
}
