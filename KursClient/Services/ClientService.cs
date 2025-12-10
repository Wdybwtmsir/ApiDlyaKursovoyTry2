using KursClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace KursClient.Services
{
    public class ClientService : BaseService<Client>
    {
        public override Task Add(Client obj)
        {
            throw new NotImplementedException();
        }

        public override Task Delete(Client obj)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<Client>> GetAll()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization",
                "Bearer " + RegisterUser.access_token);
            return (await httpClient.GetFromJsonAsync<List<Client>>("https://localhost:7291/api/Client"))!;
        }


        public override Task<List<Client>> Search(string str)
        {
            throw new NotImplementedException();
        }

        public override Task Update(Client obj)
        {
            throw new NotImplementedException();
        }
    }
}