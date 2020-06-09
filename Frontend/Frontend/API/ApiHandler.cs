using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.API
{

    public interface IApiHandler
    {
    }


    public class ApiHandler : IApiHandler
    {
        HttpClient _client;

        public ApiHandler(HttpClient client)
        {
            _client = client;
        }
    }
}