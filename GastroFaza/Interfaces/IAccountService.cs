using GastroFaza.Models;
using GastroFaza.Models.DTO;

namespace GastroFaza.Interfaces
{
    public interface IAccountServices
    {
        Task<string> GeneratJwt(LoginDto dto);
        Task RegisterUser(RegisterClientDto dto);

        Task<RegisterWorkerDto> CreateWorker();

        Task<Client> GetAccount();

        Task EditAccount();


        Task<PictureDto> SelectPicture();
    }
}
