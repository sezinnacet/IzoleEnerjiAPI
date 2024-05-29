namespace Entities.Concrete.ViewModels
{
    public class UserViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public UserDetailViewModel UserDetails { get; set; }
    }
}
