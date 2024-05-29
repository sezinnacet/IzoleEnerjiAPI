using Entities.Enums;

namespace Entities.Concrete.RequestModels
{
    public class UpgradePremiumRequestModel
    {
        public string Email { get; set; }
        public int PremiumModeId { get; set; }
        public PremiumModels Premium { get; set; }
    }
}
