

using Mapster;
using MediatR;

namespace Basket.API.Basket.GetBasket
{
    public record GetBasketRequest(string UserName) : IQuery<GetBasketResult>;
    public record GetBasketResponse(ShoppingCart Cart);
    public class GetBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{UserName}", (string UserName, ISender sender) =>
            {
                var result = sender.Send(new GetBasketRequest(UserName));
                var response = result.Adapt<GetBasketResponse>();
                return Results.Ok(response);
            }).WithName("GetBasket")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Basket Item By UserName")
            .WithDescription("Get Basket Item By UserName");
        }
    }
}
