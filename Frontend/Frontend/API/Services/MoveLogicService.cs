using System;
using System.Net.Http;
using System.Threading.Tasks;
using Frontend.API.Model;


namespace Frontend.API.Services
{
    public class MoveLogicService
    {
        private string baseUrl { get; }

        public MoveLogicService(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public async Task MoveLeftAsync(int id)
        {
            string url = baseUrl + "api/cardmovement/left/" + id;

            using (var client = new HttpClient())
            {
                await client.PutAsJsonAsync(url, id);
            }
        }
        public async Task MoveRightAsync(int id)
        {
            string url = baseUrl + "api/cardmovement/right/" + id;

            using (var client = new HttpClient())
            {
                await client.PutAsJsonAsync(url, id);
            }
        }
    }
}
