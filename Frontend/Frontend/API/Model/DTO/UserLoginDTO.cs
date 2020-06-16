namespace Frontend.API.Model.DTO
{
    public class UserLoginDTO :IEntity
    {
        public string UserEmail { get; set; }
        public string Password { get; set; }

    }
}