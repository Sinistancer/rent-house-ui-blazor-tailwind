namespace daryon_house_ui.Client.Model
{
    public class KontrakResponse
    {
        public Guid PropertyId { get; set; }
        public string UserName { get; set; }
        public int Status { get; set; }
        public string PaymentStatus { get; set; }
        public string RangeDate { get; set; }
    }
}
