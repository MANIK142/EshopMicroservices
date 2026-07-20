using BuildingBlocks.CQRS;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(string Name, List<string> Category, String Discription, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
    public record CreateProductResponse(Guid Id);
    public class CreateProductEndpoint
    {
    }
}
