namespace daryon_house_ui.Client.Application
{
    public class ToastService
    {
        public event Action<string, string> OnShow;

        public void ShowToast(string message, string type)
        {
            OnShow?.Invoke(message, type);
        }
    }
}
