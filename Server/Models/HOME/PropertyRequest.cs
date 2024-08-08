namespace daryon_house_ui.Server.Models.HOME
{
    public class PropertyRequest : IdModel
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public decimal Price { get; set; }

        public decimal Wide { get; set; }

        public int TotalRoom { get; set; }

        public string? Facility { get; set; }
    }
}
