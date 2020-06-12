namespace Frontend.API.DTO
{
    public class CardDTO : IEntity
    {
        public string Title { get; set; }
        public int ColumnId { get; set; }
        public string Info { get; set; }
    }
}