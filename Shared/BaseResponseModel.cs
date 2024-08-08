namespace daryon_house_ui.Shared
{
    public class BaseResponseModel<T>
    {
        public int TotalItems { get; set; }
        public int Code { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
