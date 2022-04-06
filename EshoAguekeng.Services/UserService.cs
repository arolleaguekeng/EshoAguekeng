using EshopAguekeng.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EshoAguekeng.Services
{
    public class UserService
    {
        private readonly HttpClient client;
        public UserService(string baseAdress)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseAdress);
        }

        public async Task<UserModel> GetAsync(int id)
        {
            //http://localhost:8180/api
            string url = $"Users/{id}";
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserModel>(data);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new KeyNotFoundException("User not found !");
            }
            else
            {
                var data = await response.Content.ReadAsStringAsync();
                throw new Exception($" Status code : {response.StatusCode} \n {data}");
            }
        }

        public async Task<UserModel> LoginAsync(string username, string password)
        {
            //http://localhost:8180/api
            string url = $"Users/?username={username}&password={password}";
            var response = await client.GetAsync(url);
            var data = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<UserModel>(data);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new UnauthorizedAccessException("Username or password is incorrect");
            }
            else
            {
                throw new Exception($" Status code : {response.StatusCode} \n {data}");
            }
        }


        public async Task<UserModel> CreateAsync(UserModel user)
        {
            //http://localhost:8180/api
            string url = $"Users";
            StringContent content = new StringContent(
                JsonConvert.SerializeObject(user),
                System.Text.Encoding.UTF8,
                "application/json"
                );
            var response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserModel>(data);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                throw new DuplicateWaitObjectException($"User name {user.Username} already exists !");
            }
            else
            {
                var data = await response.Content.ReadAsStringAsync();
                throw new Exception($" Status code : {response.StatusCode} \n {data}");
            }
        }

        public async Task<UserModel> UpdateAsync(UserModel user)
        {
            //http://localhost:8180/api
            string url = $"Users";
            StringContent content = new StringContent(
                JsonConvert.SerializeObject(user),
                System.Text.Encoding.UTF8,
                "application/json"
                );
            var response = await client.PutAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserModel>(data);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new KeyNotFoundException($"User id {user.Id} Not found !");
            }

            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                throw new DuplicateWaitObjectException($"User name {user.Username} already exists !");
            }
            else
            {
                var data = await response.Content.ReadAsStringAsync();
                throw new Exception($" Status code : {response.StatusCode} \n {data}");
            }
        }

        public async Task<UserModel> DeleteAsync(UserModel user)
        {
            //http://localhost:8180/api
            string url = $"Users";
            StringContent content = new StringContent(
                JsonConvert.SerializeObject(user),
                System.Text.Encoding.UTF8,
                "application/json"
                );
            var response = await client.PutAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserModel>(data);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new KeyNotFoundException($"User id {user.Id} Not found !");
            }

            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                throw new DuplicateWaitObjectException($"User name {user.Username} already exists !");
            }
            else
            {
                var data = await response.Content.ReadAsStringAsync();
                throw new Exception($" Status code : {response.StatusCode} \n {data}");
            }
        }
    }
}
