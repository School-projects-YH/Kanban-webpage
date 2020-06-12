using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.API.Model
{
    public class Column
    {
        public Column(string title)
        {
            Title = title;
        }
        public Column(CardDTO[] cards)
        {
            Cards = cards.ToList();
            Title = "Test";
        }
        public string Title;
        public List<CardDTO> Cards;
    }
}
