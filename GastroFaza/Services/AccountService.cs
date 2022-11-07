using GastroFaza.Authorization;
using GastroFaza.Exceptions;
using GastroFaza.Models;
using GastroFaza.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GastroFaza.Services
{

    public interface IAccountService
    {
        string GeneratJwtForClient(LoginDto dto);
        string GeneratJwtForWorker(LoginDto dto);
        void RegisterClient(RegisterClientDto dto);
        void RegisterWorker(RegisterWorkerDto dto);
    }
    public class AccountService : IAccountService
    {
        private readonly RestaurantDbContext dbContext;
        private readonly IPasswordHasher<Client> passwordHasherClient;
        private readonly IPasswordHasher<Worker> passwordHasherWorker;
        private readonly AuthenticationSettings  authenticationSetting;
        public AccountService(RestaurantDbContext dbContext, IPasswordHasher<Client> passwordHasherClient, IPasswordHasher<Worker> passwordHasherWorker, AuthenticationSettings authenticationSetting)
        {
            this.dbContext = dbContext;
            this.passwordHasherClient = passwordHasherClient;
            this.passwordHasherWorker = passwordHasherWorker;
            this.authenticationSetting = authenticationSetting;
        }
        
        public string GeneratJwtForClient(LoginDto dto)
        {
            var client = this.dbContext
                 .Clients
                 .FirstOrDefault(u => u.Email == dto.Email);


            if (client is null)
            {
                throw new BadRequestException("Invalid username or password");
            }


            var result = this.passwordHasherClient.VerifyHashedPassword(client, client.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var clasims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, client.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                //new Claim("DateOfBirth", user.DateOfBirth.Value.ToString("yyyy-MM-dd")),
                
            };


            if (!string.IsNullOrEmpty(client.Nationality))
            {
                clasims.Add(
                    new Claim("Nationality", client.Nationality)
                    );
            }



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.authenticationSetting.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(this.authenticationSetting.JwtExpireDays);


            var token = new JwtSecurityToken(this.authenticationSetting.JwtIssuer,
                this.authenticationSetting.JwtIssuer,
                clasims,
                expires: expires,
                signingCredentials: cred
                );

            var tokenHander = new JwtSecurityTokenHandler();
            return tokenHander.WriteToken(token);
        }

        public string GeneratJwtForWorker(LoginDto dto)
        {
            var worker = this.dbContext
                 .Workers
                 .Include(u => u.Role)
                 .FirstOrDefault(u => u.Email == dto.Email);


            if (worker is null)
            {
                throw new BadRequestException("Invalid username or password");
            }


            var result = this.passwordHasherWorker.VerifyHashedPassword(worker, worker.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var clasims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, worker.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{worker.FirstName} {worker.LastName}"),
                new Claim(ClaimTypes.Role, $"{worker.Role.Name} "),
                //new Claim("DateOfBirth", user.DateOfBirth.Value.ToString("yyyy-MM-dd")),
                
            };


            if (!string.IsNullOrEmpty(worker.Nationality))
            {
                clasims.Add(
                    new Claim("Nationality", worker.Nationality)
                    );
            }



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.authenticationSetting.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(this.authenticationSetting.JwtExpireDays);


            var token = new JwtSecurityToken(this.authenticationSetting.JwtIssuer,
                this.authenticationSetting.JwtIssuer,
                clasims,
                expires: expires,
                signingCredentials: cred
                );

            var tokenHander = new JwtSecurityTokenHandler();
            return tokenHander.WriteToken(token); 
        }

        public void RegisterClient(RegisterClientDto dto)
        {
            var newClient = new Client()
            {
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PasswordHash = dto.Password,

            };

            var hashedPass = this.passwordHasherClient.HashPassword(newClient, dto.Password);

            newClient.PasswordHash = hashedPass;
            this.dbContext.Clients.Add(newClient);
            this.dbContext.SaveChanges();
        }

        public void RegisterWorker(RegisterWorkerDto dto)
        {
            var newWorker = new Worker()
            {
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PasswordHash = dto.Password,
                RoleId = dto.RoleId

            };

            var hashedPass = this.passwordHasherWorker.HashPassword(newWorker, dto.Password);

            newWorker.PasswordHash = hashedPass;
            this.dbContext.Workers.Add(newWorker);
            this.dbContext.SaveChanges();
        }
    }
}
