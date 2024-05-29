namespace Entities.Concrete.ViewModels
{
    public class UserDetailViewModel
    {
        public bool IsPremium { get; set; }
        public DateTime? PremiumEndDate { get; set; }
        public PremiumModeViewModel? PremiumMode { get; set; }
    }
}
