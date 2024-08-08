using Newtonsoft.Json.Linq;

namespace daryon_house_ui.Shared
{
    public class BaseListResponseModel<T>
    {
        public int TotalItems { get; set; }
        public int Code { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public List<T> Data { get; set; }
    }
}
