namespace Biblioteka.Models
{
    public class ErrorViewModel
    {
        int x = 0;
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
