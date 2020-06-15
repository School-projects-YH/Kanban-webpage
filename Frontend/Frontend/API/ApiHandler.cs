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

        private string baseUrl = "https://localhost:9001/";
        private string uri = "";
        private string url
        { get { return baseUrl + uri; } }

        /* ------------------------------- Constructor ------------------------------ */

        public ApiHandler()
        {
            _client = new HttpClient();
            cardService = new CardService(_client);
        }

        public ApiHandler(HttpClient client) : this()
        {
            _client = client;
        }

        /* ----------------------------- End Constructor ---------------------------- */

        /* ---------------------------------- Board --------------------------------- */

        public async Task<BoardDTO> CreateBoard(string title)
        {
            string url = "http://localhost:9000/api/board/";

            var board = new BoardDTO()
            {
                Title = title
            };

            var response = await _client.PostAsJsonAsync(url, board);

            if (response.IsSuccessStatusCode)
            {
                var uri = response.Headers.Location.ToString();
                string id = uri.Substring(uri.LastIndexOf('/') + 1);
                board.Id = Convert.ToInt32(id);

                return board;
            }
            else
            {
                return null;
            }
        }

        public async Task<BoardDTO[]> GetBoardsAsync(string url = "https://localhost:9001/api/board")
        {
            HttpResponseMessage response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var board = await response.Content.ReadAsAsync<BoardDTO[]>();
                return board;
            }
            return null;
        }

        /* -------------------------------- End Board ------------------------------- */

        /* -------------------------------- MoveLogic ------------------------------- */

        public async Task MoveLeftAsync(int id)
        {
            string url = "https://localhost:9001/api/cardmovement/left";

            await _client.PutAsJsonAsync(url, id);
        }

        public async Task MoveRightAsync(int id)
        {
            string url = "https://localhost:9001/api/cardmovement/right";

            await _client.PutAsJsonAsync(url, id);
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