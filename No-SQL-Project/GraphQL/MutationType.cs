using No_SQL_Project.GraphQL.Movies;
using No_SQL_Project.GraphQL.Types;

namespace No_SQL_Project.GraphQL;

public class MutationType : ObjectTypeExtension<Mutation>
{
    protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
    {
        descriptor.Field(f => f.CreateMovie(default, default))
            .Type<NonNullType<MovieGraphType>>();

        descriptor.Field(f => f.UpdateMovie(default, default, default))
            .Type<NonNullType<MovieGraphType>>();
        
        
    }
}