using daryon_house_ui.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daryon_house_ui.Shared
{
    public class Tenant
    {
        public Guid Id { get; set; }
        public TenantStatus Status { get; set; }
        public string StatusDisplay { get; set; }
        public bool PaymentStatus { get; set; }
        public DateTime StarDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid PropertyId { get; set; }
    }
}
