using No_SQL_Project.GraphQL.Movies;
using No_SQL_Project.GraphQL.Types;

namespace No_SQL_Project.GraphQL;

public class QueryType : ObjectType<Query>
{
    protected override void Configure([Service] IObjectTypeDescriptor<Query> descriptor)
    {
        descriptor
            .Field(f => f.GetMoviesAsync(default))
            .Type<ListType<MovieGraphType>>();
    }
}