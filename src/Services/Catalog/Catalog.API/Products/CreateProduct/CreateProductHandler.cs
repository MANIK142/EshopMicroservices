namespace Catalog.API.Products.CreateProduct;
public record CreateProductCommand(string Name,List<string> Category,String Discription, string ImageFile,decimal Price) : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);
internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Discription,
            Price = command.Price,
            ImageFile = command.ImageFile,
        };

        // Save to DB
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        // Return Result
        return new CreateProductResult(product.Id);
    }
}
 