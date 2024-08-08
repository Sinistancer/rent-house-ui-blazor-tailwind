namespace daryon_house_ui.Shared
{
    public class WaterUsed
    {
        public string HouseNumber { get; set; }

        public decimal Price { get; set; }
        public double WaterUsedPrevMonth { get; set; }

        public double WaterUsedCurrentMonth { get; set; }
        public double TotalWaterUsed => (double)(WaterUsedCurrentMonth - WaterUsedPrevMonth);

        public decimal TotalPrice => Price * (decimal)(WaterUsedCurrentMonth - WaterUsedPrevMonth);
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
