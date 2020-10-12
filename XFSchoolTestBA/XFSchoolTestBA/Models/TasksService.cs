using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace XFSchoolTestBA.Models
{
    class TasksService
    {
        const string Url = "http://pakdmitriy1989-001-site1.htempurl.com/api/Tasks";

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }


        public async Task<IEnumerable<Tasks>> GetTasks(string login, string password)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url + "/" + login + "/" + password);
            return JsonConvert.DeserializeObject<IEnumerable<Tasks>>(result);
        }


        public async Task<Tasks> GetTasksID(string login, string password,int id)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url + "/" + login + "/" + password+ "/"+id);
            return JsonConvert.DeserializeObject<Tasks>(result);
        }



        public async Task<IEnumerable<Tests>> GetTaskName(string login, string password)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url + "/" + login + "/" + password+"/TaskName");
            return JsonConvert.DeserializeObject<IEnumerable<Tests>>(result);
        }







        // добавляем одного друга
        public async Task<Tasks> AddTask(Tasks task)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(Url,
                new StringContent(
                    JsonConvert.SerializeObject(task),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Tasks>(
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
