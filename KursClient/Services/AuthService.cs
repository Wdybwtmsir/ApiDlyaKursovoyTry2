using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using KursClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KursClient.Services
{
    public class AuthService
    {
        private HttpClient client = new HttpClient();
        public async Task<String> Register(Admin admin)
        {
            JsonContent content = JsonContent.Create(admin);
            using var response = await client.PostAsync("https://localhost:7291/register", content);
            string responseText = await response.Content.ReadAsStringAsync();
            if (responseText != "")
            {
                return $"Пользователь {admin.Email} успешно создан";
            }
            return $"Пользователь {admin.Email} существует!";
        }
        public async Task<Response> SignIn(Admin admin)
        {
            JsonContent content = JsonContent.Create(admin);
            using var response = await client.PostAsync("https://localhost:7291/login", content);
            string responseText = await response.Content.ReadAsStringAsync();
            if (responseText != "")
            {
                Response resp = JsonSerializer.Deserialize<Response>(responseText)!;
                return resp;
            }
            return null!;
        }
    }
}