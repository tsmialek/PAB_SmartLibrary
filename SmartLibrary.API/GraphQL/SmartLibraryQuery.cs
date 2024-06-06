using GraphQL;
using GraphQL.Types;
using SmartLibrary.Application.Services.BookMenagement;

namespace SmartLibrary.API.GraphQL
{
    public class SmartLibraryQuery : ObjectGraphType
    {
        public SmartLibraryQuery(IBookService bookService)
        {
            Field<ListGraphType<BookType>>("books")
                .Resolve(context => bookService.GetBooks());

            Field<BookType>("book")
                .Arguments(new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }))
                .Resolve(context => bookService.GetBookById(context.GetArgument<Guid>("id")));
        }
    }
}
