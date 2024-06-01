namespace SmartLibrary.Contracts.Book
{
    public record BookResponse(
    Guid Id,
    string Title,
    string Author,
    string ISBN,
    string? Description,
    int? PageCount,
    DateOnly? Date);
}
