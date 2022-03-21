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
        public UserService(string baseAddress)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
        }

        public async Task<UserModel> Login(string username, string password)
        {
            //http://localhost:8081/api
            string url = $"/Users/username={username}&password={password}";
            var response = await client.GetAsync(url);
            var data = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {

                return JsonConvert.DeserializeObject<UserModel>(data);
            }
            else if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new UnauthorizedAccessException("Username or Password is incorrect");
            }
            else
            {
                throw new Exception($"Error Status  code: {response.StatusCode} \n {data}");
            }
        }

        public async Task<UserModel> Get(int id)
        {
            //http://localhost:8081/api
            string url = $"/Users/{id}";
            var response = await client.GetAsync(url);
            var data = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {

                return JsonConvert.DeserializeObject<UserModel>(data);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new KeyNotFoundException("User not found !");
            }
            else
            {
                throw new Exception($"Error Status  code: {response.StatusCode} \n {data}");
            }
        }

        public async Task<UserModel> Create(UserModel user)
        {
            //http://localhost:8081/api
            string url = $"/Users";
            StringContent content = new StringContent
            (
                JsonConvert.SerializeObject(user),
                System.Text.Encoding.UTF8,
                "application/json"
            );
            var response = await client.PostAsync(url, content);
            var data = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {

                return JsonConvert.DeserializeObject<UserModel>(data);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                throw new UnauthorizedAccessException("Username or Password is incorrect");
            }
            else
            {
                throw new UnauthorizedAccessException($"User name {user.Username} already exist !");
            }
        }

        public async Task<UserModel> Update(UserModel user)
        {
            //http://localhost:8081/api
            string url = $"/Users";
            StringContent content = new StringContent
            (
                JsonConvert.SerializeObject(user),
                System.Text.Encoding.UTF8,
                "application/json"
            );
            var response = await client.PostAsync(url, content);
            var data = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {

                return JsonConvert.DeserializeObject<UserModel>(data);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new KeyNotFoundException($"User id {user.Id} not found !  ");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                throw new UnauthorizedAccessException($"User name {user.Username} already exist !");
            }

            else
            {
                throw new Exception($"Error Status  code: {response.StatusCode} \n {data}");
            }
        }




    }
}
