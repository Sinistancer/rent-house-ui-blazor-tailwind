using daryon_house_ui.Shared.Enum;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace daryon_house_ui.Shared
{
    public class Kontrak
    {
        //User Info
        public string Name { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Address { get; set; }
        public string Locality { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string UserName { get; set; }

        //Kontrak Info
        public TenantStatus Status { get; set; }
        public string StatusDisplay { get; set; }
        public string PaymentStatusDisplay { get; set; }
        public bool PaymentStatus { get; set; }
        public DateTime StarDate { get; set; }
        public DateTime EndDate { get; set; }
        public string RangeDate { get; set; }

        //Home Info
        public Guid PropertyId { get; set; }
        public string HomeName { get; set; }
        public string HomeAddress { get; set; }
        public decimal Price { get; set; }
        public decimal Wide { get; set; }
        public int TotalRoom { get; set; }
        public string? Facility { get; set; }
    }
}
