using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.API.Model
{
    public class Board
    {
        private Board()
        {

        }

        public Board(int id) : this()
        {
            for(int i = 0; i < 4; i++)
            {
                columns.Add(new Column("Test"));
            }
        }

        public List<Column> columns = new List<Column>();
    }
}
