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

        public async Task<Client> GetClientByIdAsyn(int id)
        {
            return await _dataContext.Clients.SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<bool> CreateClientAsyn(Client client)
        {
            await _dataContext.Clients.AddAsync(client);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> UpdateClientAsyn(Client clientToUpdate)
        {
            _dataContext.Clients.Update(clientToUpdate);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteClientAsyn(int id)
        {
            var client = await GetClientByIdAsyn(id);

            if (client == null)
                return false;

            _dataContext.Clients.Remove(client);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}
