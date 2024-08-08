namespace daryon_house_ui.Shared
{
    public class Property : PropertyDropdown
    {
        public string Address { get; set; }
        public decimal Price { get; set; }
        public decimal Wide { get; set; }
        public int TotalRoom { get; set; }
        public string? Facility { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatorUserName { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string LastModifierUserName { get; set; }
    }
}
