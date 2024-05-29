namespace Entities.Concrete.RequestModels
{
    public class UserDetailRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
    }
}
