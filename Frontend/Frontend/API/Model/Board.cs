using System.Collections.Generic;

namespace Frontend.API.Model
{
    public class Board
    {
        private Board()
        {
        }

        public Board(int id) : this()
        {
            for (int i = 0; i < 4; i++)
            {
                columns.Add(new Column("Test"));
            }
        }

        public List<Column> columns = new List<Column>();
    }
}