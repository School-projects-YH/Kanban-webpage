using System.Collections.Generic;
using System.Threading.Tasks;
using Frontend.API;

namespace Frontend.API.Model
{
    public class Board
    {
        private Board()
        {
            boardDTO = new BoardDTO();
            columns = new List<ColumnDTO>();
        }

        private readonly BoardDTO boardDTO;
        public IEnumerable<ColumnDTO> columns;
        public Board(int boardId) : this()
        {
            boardDTO.Id = boardId;
        }

      
    }
}