namespace daryon_house_ui.Client.Model
{
    public class PropertyRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public string Wide { get; set; }
        public string TotalRoom { get; set; }
        public string? Facility { get; set; }
    }
}
