namespace Frontend.API.Model
{
    public class UserLoginDTO :IEntity
    {
        public string UserEmail { get; set; }
        public string Password { get; set; }

    }
}