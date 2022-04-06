using EshopAguekeng.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EshoAguekeng.Services
{
    public class ProductService
    {
        private readonly HttpClient client;
        public ProductService(string baseAdress)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseAdress);
        }



        public async Task<IEnumerable<ProductModel>> GetAsync()
        {
            //http://localhost:8180/api
            string url = $"Products";
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<ProductModel>>(data);
            }

            else
            {
                var data = await response.Content.ReadAsStringAsync();
                throw new Exception($" Status code : {response.StatusCode} \n {data}");
            }
        }

        public async Task<ProductModel> GetAsync(int id)
        {
            //http://localhost:8180/api
            string url = $"Products/{id}";
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ProductModel>(data);
            }
            else
            {
                var data = await response.Content.ReadAsStringAsync();
                throw new Exception($" Status code : {response.StatusCode} \n {data}");
            }
        }

        


        public async Task<ProductModel> CreateAsync(ProductModel product , byte[] photo )
        {
            //http://localhost:8180/api
            string url = $"Products";
            StringContent content = new StringContent(
                JsonConvert.SerializeObject(product),
                System.Text.Encoding.UTF8,
                "application/json"
                );
            var response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ProductModel>(data);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                throw new DuplicateWaitObjectException($"product name {product.Name} already exists !");
            }
            else
            {
                var data = await response.Content.ReadAsStringAsync();
                throw new Exception($" Status code : {response.StatusCode} \n {data}");
            }
        }

        public async Task<ProductModel> UpdateAsync(ProductModel product)
        {
            //http://localhost:8180/api
            string url = $"Products";
            StringContent content = new StringContent(
                JsonConvert.SerializeObject(product),
                System.Text.Encoding.UTF8,
                "application/json"
                );
            var response = await client.PutAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ProductModel>(data);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new KeyNotFoundException($"product id {product.Id} Not found !");
            }

            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                throw new DuplicateWaitObjectException($"product name {product.Name} already exists !");
            }
            else
            {
                var data = await response.Content.ReadAsStringAsync();
                throw new Exception($" Status code : {response.StatusCode} \n {data}");
            }
        }

        public async Task<ProductModel> DeleteAsync(ProductModel product)
        {
            //http://localhost:8180/api
            string url = $"Products";
            StringContent content = new StringContent(
                JsonConvert.SerializeObject(product),
                System.Text.Encoding.UTF8,
                "application/json"
                );
            var response = await client.PutAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ProductModel>(data);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new KeyNotFoundException($"product id {product.Id} Not found !");
            }

            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                throw new DuplicateWaitObjectException($"product name {product.Name} already exists !");
            }
            else
            {
                var data = await response.Content.ReadAsStringAsync();
                throw new Exception($" Status code : {response.StatusCode} \n {data}");
            }
        }
    }
}
