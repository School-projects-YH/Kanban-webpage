namespace Backend
{
    public class UserBoards
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User{get;set; }
        public int BoardId { get; set; }
        public Board Board {get;set;}
       
      
    }
}