using System.Collections.Generic;
using System.Linq;

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