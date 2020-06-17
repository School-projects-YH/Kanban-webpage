using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.API.Model
{
    public class ColumnDTO : IEntity
    {
        public ColumnDTO()
        {
            Cards = new List<CardDTO>();
        }
        public string Title;
        public List<CardDTO> Cards;
    }
}
