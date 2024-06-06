using GraphQL.Types;

namespace SmartLibrary.API.GraphQL
{
    public class SmartLibrarySchema : Schema
    {
        public SmartLibrarySchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<SmartLibraryQuery>();
        }
    }
}
