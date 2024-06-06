using GraphQL.Types;
using SmartLibrary.Domain.Entities;

namespace SmartLibrary.API.GraphQL
{
    public class BookType : ObjectGraphType<Book>
    {
        public BookType()
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("The ID of the book.");
            Field(x => x.Title).Description("The title of the book.");
            Field(x => x.Author).Description("The author of the book.");
            Field(x => x.ISBN).Description("The ISBN of the book.");
            Field(x => x.Description, nullable: true).Description("The description of the book.");
            Field(x => x.PageCount, nullable: true).Description("The number of pages in the book.");
            Field(x => x.Date, nullable: true).Description("The publication date of the book.");
        }
    }
}
