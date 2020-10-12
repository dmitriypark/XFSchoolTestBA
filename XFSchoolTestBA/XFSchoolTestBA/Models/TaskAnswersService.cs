using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace XFSchoolTestBA.Models
{
    class TaskAnswersService
    {
        const string Url = "http://pakdmitriy1989-001-site1.htempurl.com/api/TaskAnswers";

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }


        public async Task<IEnumerable<TaskAnswers>> GetTaskAnswers(string login, string password,int id)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url + "/" + login + "/" + password+"/"+id);
            return JsonConvert.DeserializeObject<IEnumerable<TaskAnswers>>(result);
        }


        public async Task<IEnumerable<Questions>> GetTaskAnswersQuestion(string login, string password, int id)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url + "/questionContent/" + login + "/" + password + "/" + id);
            return JsonConvert.DeserializeObject<IEnumerable<Questions>>(result);
        }





        // добавляем одного друга
        public async Task<TaskAnswers> AddTaskAnswers(TaskAnswers taskAnswers)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(Url,
                new StringContent(
                    JsonConvert.SerializeObject(taskAnswers),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<TaskAnswers>(
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
