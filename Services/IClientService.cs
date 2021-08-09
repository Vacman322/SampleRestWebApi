using SampleRestWebApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleRestWebApi.Services
{
    public interface IClientService
    {
        Task<List<Client>> GetAllAsync();

        Task<Client> GetClientByIdAsync(int id);

        Task<bool> UpdateClientAsyn(Client clientToUpdate);

        Task<bool> CreateClientAsyn(Client clientToUpdate);


        Task<bool> DeleteClientAsyn(int id);
        Task<bool> UserOwnClientAsync(int clientId, string userId);
    }
}
