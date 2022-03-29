using EshopAguekeng.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EshoAguekeng.Services
{
    public class CategoryService
    {
        private readonly HttpClient client;
        public CategoryService(string baseAdress)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseAdress);
        }

        public async Task<IEnumerable<CategoryModel>> GetAsync()
        {
            //http://localhost:8180/api
            string url = $"/Categorys";
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<CategoryModel>>(data);
            }
            
            else
            {
                var data = await response.Content.ReadAsStringAsync();
                throw new Exception($" Status code : {response.StatusCode} \n {data}");
            }
        }
    }
}
