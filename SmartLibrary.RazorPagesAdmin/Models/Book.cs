namespace SmartLibrary.RazorPagesAdmin.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Description { get; set; }
        public int? PageCount { get; set; }
        public DateTime? Date { get; set; }
    }
}
