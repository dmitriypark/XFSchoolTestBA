using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace XFSchoolTestBA.Models
{
    class QuestionsService
    {
        const string Url = "http://pakdmitriy1989-001-site1.htempurl.com/api/questions";

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }


        public async Task<IEnumerable<Questions>> GetQuestions(string login, string password, int id)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url + "/" + login + "/" + password + "/" + id);
            return JsonConvert.DeserializeObject<IEnumerable<Questions>>(result);
        }
    }
}
