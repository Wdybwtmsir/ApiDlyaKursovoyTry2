using KursClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace KursClient.Services
{
    public class ClientService : BaseService<Client>
    {
        private HttpClient httpClient;
        public ClientService()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization",
               "Bearer " + RegisterUser.access_token);
        }
        public override async Task Add(Client obj)
        {
            try
            {
                JsonContent content = JsonContent.Create(obj);
                using var response = await httpClient.PostAsync("https://localhost:7291/API/Client", content);
                string responseText = await response.Content.ReadAsStringAsync();
                if (responseText != null)
                {
                    Client resp = JsonSerializer.Deserialize<Client>(responseText!)!;
                    if (resp == null) MessageBox.Show(responseText);
                }
            }
            catch { }
        }

        public override async Task Delete(Client obj)
        {
            using var response = await httpClient.DeleteAsync($"https://localhost:7291/API/Client{obj.IdClient}");

        }

        public override async Task<List<Client>> GetAll()
        {
            return (await httpClient.GetFromJsonAsync<List<Client>>("https://localhost:7291/API/Client"))!;
        }


        public override Task<List<Client>> Search(string str)
        {
            throw new NotImplementedException();
        }

        public override async Task Update(Client obj)
        {
            try
            {
                JsonContent content = JsonContent.Create(obj);
                using var response = await httpClient.PutAsync($"https://localhost:7291/API/Client{obj.IdClient}", content);
                string responseText = await response.Content.ReadAsStringAsync();
                if (responseText != null)
                {
                    Client resp = JsonSerializer.Deserialize<Client>(responseText!)!;
                    if (resp == null) MessageBox.Show(responseText);
                }

            }
            catch { }
        }
    }
}