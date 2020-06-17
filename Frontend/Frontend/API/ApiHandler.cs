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
        public MoveLogicService moveLogicService { get; }

        // Ändra denna när ni vill byta port! (https/http)
        private string baseUrl = PortUrl.https;
        
        private string uri = "";
        private string url
        { get { return baseUrl + uri; } }

        /* ------------------------------- Constructor ------------------------------ */

        public ApiHandler()
        {
            _client = new HttpClient();
            
            // Connect to services
            cardService = new CardService(baseUrl);
            columnService = new ColumnService(baseUrl);
            boardService = new BoardService(baseUrl);
            userService = new UserService(baseUrl);
            moveLogicService = new MoveLogicService(baseUrl);
        }

        public ApiHandler(HttpClient client) : this()
        {
            _client = client;
        }

        /* ----------------------------- End Constructor ---------------------------- */


        /* --------------------------------- Dispose -------------------------------- */

        public void Dispose() => Dispose(true);

        protected void Dispose(bool disposing)
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
            return;
        }

        /* ------------------------------- End Dispose ------------------------------ */
    }
}