namespace BackendService.Models.Request
{
    public class BookRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
