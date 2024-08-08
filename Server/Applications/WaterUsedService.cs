using daryon_house_ui.Shared;
using Newtonsoft.Json;

namespace daryon_house_ui.Server.Applications
{
    public class WaterUsedService
    {
        private readonly ILogger<WaterUsedService> _logger;
        public WaterUsedService(ILogger<WaterUsedService> logger)
        {
            _logger = logger;
        }

        public List<WaterUsed>? GetWaterUsed(int month, int year)
        {
            string jsonData = System.IO.File.ReadAllText("Data/water-used.json");

            List<WaterUsed>? bindingData = JsonConvert.DeserializeObject<List<WaterUsed>>(jsonData);

            return bindingData?.Where(x => x.Month == month && x.Year == year).ToList();
        }

        public List<string>? GetYearMonth()
        {
            string jsonData = System.IO.File.ReadAllText("Data/water-used.json");

            List<WaterUsed>? bindingData = JsonConvert.DeserializeObject<List<WaterUsed>>(jsonData);

            var data = bindingData?.Select(x => new { x.Month, data = $"{x.Month}|{x.Year}" }).DistinctBy(x => x.Month);

            return data?.Select(x => x.data).ToList();
        }
    }
}
