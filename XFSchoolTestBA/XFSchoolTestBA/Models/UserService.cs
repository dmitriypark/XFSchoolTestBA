using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace XFSchoolTestBA.Models
{
    class UserService
    {
        const string Url = "http://pakdmitriy1989-001-site1.htempurl.com/api/user";

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }


        public async Task<IEnumerable<User>> Get()
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url);
            return JsonConvert.DeserializeObject<IEnumerable<User>>(result);
        }


        //проверяем пользователя в базе данных
        public async Task<User> GetCheck(string login, string password)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url + "/check" + "/" + login + "/" + password);
            return JsonConvert.DeserializeObject<User>(result);
        }



        // добавляем одного друга
        public async Task<User> Add(User user)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(Url,
                new StringContent(
                    JsonConvert.SerializeObject(user),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<User>(
                await response.Content.ReadAsStringAsync());
        }
        // обновляем друга
        public async Task<User> Update(User user)
        {
            HttpClient client = GetClient();
            var response = await client.PutAsync(Url + "/" + user.Id,
                new StringContent(
                    JsonConvert.SerializeObject(user),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<User>(
                await response.Content.ReadAsStringAsync());
        }
        // удаляем друга
        public async Task<User> Delete(int id)
        {
            HttpClient client = GetClient();
            var response = await client.DeleteAsync(Url + "/" + id);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<User>(
               await response.Content.ReadAsStringAsync());
        }
    }
}
