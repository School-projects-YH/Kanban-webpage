using Frontend.API.Model;
using Frontend.API.Services;
using System; 
using System.Net.Http;
using System.Threading.Tasks; 


namespace Frontend.API
{
    public partial class ApiHandler : IDisposable
    {
        /* -------------------------------------------------------------------------- */
        /*                                 API Handler                                */
        /* -------------------------------------------------------------------------- */

        private HttpClient _client;
        private bool _disposed = false;
        public CardService cardService { get; }
        public ColumnService columnService { get; }
        public BoardService boardService { get; }
        public UserService userService { get; }


        //private string baseUrl = "http://localhost:9000/"; 

        private string baseUrl = "https://localhost:9001/";
        private string uri = "";
        private string url
        { get { return baseUrl + uri; } }

        /* ------------------------------- Constructor ------------------------------ */

        public ApiHandler()
        {
            _client = new HttpClient();
            
            // Connect to services
            cardService = new CardService(_client);
            columnService = new ColumnService(_client);
            boardService = new BoardService(_client);
            userService = new UserService(_client);
        }

        public ApiHandler(HttpClient client) : this()
        {
            _client = client;
        }

        /* ----------------------------- End Constructor ---------------------------- */

        /* -------------------------------- MoveLogic ------------------------------- */

        public async Task MoveLeftAsync(int id)
        {
            
             string url = baseUrl + "api/cardmovement/left/" + id ;

            var client = new HttpClient();


            await client.PutAsJsonAsync(url, id);
        }

        public async Task MoveRightAsync(int id)
        {
            

            string url = baseUrl + "api/cardmovement/right/" + id ;

            var client = new HttpClient();


            await client.PutAsJsonAsync(url, id);
           
        }

        /* ------------------------------ End MoveLogic ----------------------------- */

        /* --------------------------------- Dispose -------------------------------- */

        public void Dispose() => Dispose(true);

        public void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _client?.Dispose();
            }
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        /* ------------------------------- End Dispose ------------------------------ */
    }
}