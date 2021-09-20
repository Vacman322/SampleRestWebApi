using Microsoft.EntityFrameworkCore;
using SampleRestWebApi.Data;
using SampleRestWebApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleRestWebApi.Services
{
    public class ClientService : IClientService
    {
        private readonly DataContext _dataContext;

        public ClientService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Client>> GetAllAsync()
        {
            return await _dataContext.Clients.ToListAsync();
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            return await _dataContext.Clients.SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<bool> CreateClientAsync(Client client)
        {
            await _dataContext.Clients.AddAsync(client);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> UpdateClientAsync(Client clientToUpdate)
        {
            _dataContext.Clients.Update(clientToUpdate);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
            var client = await GetClientByIdAsync(id);

            if (client == null)
                return false;

            _dataContext.Clients.Remove(client);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> UserOwnClientAsync(int clientId, string userId)
        {
            var client = await _dataContext.Clients.AsNoTracking().SingleOrDefaultAsync(x => x.Id == clientId);

            if(client == null)
            {
                return false;
            }

            return client.UserId == userId;
        }

        public async Task<List<Tag>> GetAllTagsAsync()
        {
            return await _dataContext.Tags.ToListAsync();
        }

        public async Task<Tag> GetTagByNameAsync(string name)
        {
            return await _dataContext.Tags.SingleOrDefaultAsync(r => r.Name == name);
        }

        public async Task<bool> CreateTagAsync(Tag tagToAdd)
        {
            await _dataContext.Tags.AddAsync(tagToAdd);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> DeleteTagAsync(string tagName)
        {
            var tag = await GetTagByNameAsync(tagName);

            if (tag == null)
                return false;

            _dataContext.Tags.Remove(tag);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}
