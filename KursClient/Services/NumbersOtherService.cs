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
    public class NumbersOtherService : BaseService<NumbersOther>
    {
        public override Task Add(NumbersOther obj)
        {
            throw new NotImplementedException();
        }

        public override Task Delete(NumbersOther obj)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<NumbersOther>> GetAll()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization",
                "Bearer " + RegisterUser.access_token);
            return (await httpClient.GetFromJsonAsync<List<NumbersOther>>("https://localhost:7291/api/NumbersOther"))!;
        }


        public override Task<List<NumbersOther>> Search(string str)
        {
            throw new NotImplementedException();
        }

        public override Task Update(NumbersOther obj)
        {
            throw new NotImplementedException();
        }
    }
}