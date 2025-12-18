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
    public class NumbersOtherService : BaseService<NumbersOther>
    {
        private HttpClient httpClient;
        public NumbersOtherService()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization",
               "Bearer " + RegisterUser.access_token);
        }
        public override async Task Add(NumbersOther obj)
        {
            try
            {
                JsonContent content = JsonContent.Create(obj);
                using var response = await httpClient.PostAsync("https://localhost:7291/api/NumbersOther", content);
                string responseText = await response.Content.ReadAsStringAsync();
                if (responseText != null)
                {
                    NumbersOther resp = JsonSerializer.Deserialize<NumbersOther>(responseText!)!;
                    if (resp == null) MessageBox.Show(responseText);
                }
            }
            catch { }
        }

        public override async Task Delete(NumbersOther obj)
        {
            using var response = await httpClient.DeleteAsync($"https://localhost:7291/api/NumbersOther{obj.IdNumbersOther}");

        }

        public override async Task<List<NumbersOther>> GetAll()
        {
            return (await httpClient.GetFromJsonAsync<List<NumbersOther>>("https://localhost:7291/api/NumbersOther"))!;
        }


        public override Task<List<NumbersOther>> Search(string str)
        {
            throw new NotImplementedException();
        }

        public override async Task Update(NumbersOther obj)
        {
            try
            {
                JsonContent content = JsonContent.Create(obj);
                using var response = await httpClient.PutAsync($"https://localhost:7291/API/NumbersOther{obj.IdNumbersOther}", content);
                string responseText = await response.Content.ReadAsStringAsync();
                if (responseText != null)
                {
                    NumbersOther resp = JsonSerializer.Deserialize<NumbersOther>(responseText!)!;
                    if (resp == null) MessageBox.Show(responseText);
                }

            }
            catch { }
        }
    }
}