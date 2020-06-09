namespace Backend
{
    public class Card
    {
        public int Id { get; set; }
        public int ColumnId { get; set; }
        public Column Column {get; set;}
        public int BoardId { get; set; }
        public Board Board {get; set;}
        public string Info { get; set; }
    }
}