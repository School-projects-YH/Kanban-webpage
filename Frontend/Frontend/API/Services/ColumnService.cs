using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

using Frontend.API.Model;

namespace Frontend.API.Services
{
    public class ColumnService : IService<ColumnDTO>
    {
        private HttpClient _httpClient;
        //private string baseUrl = "http://localhost:9000/";
        private string baseUrl = "https://localhost:9001/";
        private string uri = "api/column/";
        private string url
        { get { return baseUrl + uri; } }

        public ColumnService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ColumnDTO>> GetAll()
        {
            using (_httpClient)
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var columns = await response.Content.ReadAsAsync<IEnumerable<ColumnDTO>>();
                    return columns;
                }
                else
                {
                    throw new Exception("Hahah du krashade!");
                }
            }
        }

        public async Task<IEnumerable<ColumnDTO>> GetColumnsByBoardIdAsync(int id)
        {
            using(_httpClient)
            {
                var response = await _httpClient.GetAsync(url + "Board/" + id);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(await response.Content.ReadAsStringAsync());
                    var columnDTOs = await response.Content.ReadAsAsync<ColumnDTO[]>();
                    
                    return columnDTOs;
                }
                throw new Exception("Get columns by board id unsuccesfull");
            }
        }
        // Create
        public async Task<int> Create(ColumnDTO columnDTO)
        {
            using (_httpClient)
            {
                var response = await _httpClient.PostAsJsonAsync(url, columnDTO);
                if (response.IsSuccessStatusCode)
                {
                    var uri = response.Headers.Location.ToString();
                    string stringId = uri.Substring(uri.LastIndexOf('/') + 1);
                    var id = Convert.ToInt32(stringId);
                    return id;
                }
                throw new Exception("Create column not succesfull");
            }
        }

        // Delete

        public async Task Delete(ColumnDTO columnDTO)
        {
            using (_httpClient)
            {
                var response = await _httpClient.DeleteAsync(url + columnDTO.Id);
            }
        }

        // Update
        public async Task<ColumnDTO> Update(ColumnDTO columnDTO)
        {
            using (_httpClient)
            {
                var response = await _httpClient.PutAsJsonAsync(url + columnDTO.Id, columnDTO);
                var column = await response.Content.ReadAsAsync<ColumnDTO>();
                return column;
            }
        }

        // Find by id

        public async Task<ColumnDTO> FindById(int id)
        {
            using (_httpClient)
            {
                var response = await _httpClient.GetAsync(url + id);
                var column = await response.Content.ReadAsAsync<ColumnDTO>();
                return column;
            }
        }
    }
}

