using daryon_house_ui.Shared.Enum;

namespace daryon_house_ui.Client.Model
{
    public class KontrakRequest
    {
        public Guid PropertyId { get; set; }
        public string UserName { get; set; }
        public TenantStatus Status { get; set; }
        public bool PaymentStatus { get; set; }
        public DateTime StarDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
