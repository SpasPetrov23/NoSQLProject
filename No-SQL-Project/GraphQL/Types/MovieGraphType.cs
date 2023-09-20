using No_SQL_Project.Models;

namespace No_SQL_Project.GraphQL.Types;

public class MovieGraphType : ObjectType<Movie>
{
    protected override void Configure(IObjectTypeDescriptor<Movie> descriptor)
    {
        descriptor.Field(f => f.Id);
        descriptor.Field(f => f.Runtime);
        descriptor.Field(f => f.Title);
        descriptor.Field(f => f.Year);
        descriptor.Field(f => f.Rating);
        descriptor.Field(f => f.Cast).Type<ListType<StringType>>();
        descriptor.Field(f => f.Directors).Type<ListType<StringType>>();
        descriptor.Field(f => f.Genres).Type<ListType<StringType>>();
    }
}