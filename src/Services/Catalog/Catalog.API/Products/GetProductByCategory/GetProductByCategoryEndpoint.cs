
using System.Collections;
using Catalog.API.Products.GetProductById;

namespace Catalog.API.Products.GetProductByCategory
{
    //public record GetProductCategoryRequest(string category) : IQuery<GetProductByCategoryResponse>;
    public record GetProductByCategoryResponse(IEnumerable<Product> Products);
    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {
                var results = await sender.Send(new GetProductByCategoryQuery(category));
                var response = results.Adapt<GetProductByCategoryResponse>();
                return Results.Ok(response);
            }).WithName("GetProductByCategoryEndpoint")
                .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Product by Category")
                .WithDescription("Get Product by Category"); ;
        }
    }
}
