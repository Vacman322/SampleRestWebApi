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
        Task<List<Tag>> GetAllTagsAsync();

        Task<Client> GetClientByIdAsync(int id);
        Task<Tag> GetTagByNameAsync(string name);

        Task<bool> UpdateClientAsync(Client clientToUpdate);

        Task<bool> CreateClientAsync(Client clientToAdd);
        Task<bool> CreateTagAsync(Tag tagToAdd);

        Task<bool> DeleteClientAsync(int id);
        Task<bool> DeleteTagAsync(string name);

        Task<bool> UserOwnClientAsync(int clientId, string userId);


    }
}
