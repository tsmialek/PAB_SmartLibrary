namespace SmartLibrary.Domain.Entities
{
    public class Book 
    { 
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string ISBN { get; set; } = null!;
        public string? Description { get; set; }
        public int? PageCount { get; set; }
        public DateOnly? Date { get; set; }
    }
}
