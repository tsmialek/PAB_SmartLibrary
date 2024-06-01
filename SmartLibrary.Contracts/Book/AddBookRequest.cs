namespace SmartLibrary.Contracts.Book
{
    public class AddBookRequest
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string? Description { get; set; }
        public int? PageCount { get; set; }
        public DateOnly? Date { get; set; }
    }
}
