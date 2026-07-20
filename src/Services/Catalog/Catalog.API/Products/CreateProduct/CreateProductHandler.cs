using Microsoft.Win32;
using MediatR;
using System.Windows.Input;
using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct;
public record CreateProductCommand(string Name,List<string> Category,String Discription, string ImageFile,decimal Price) : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);
internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
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

        // Return Result
        return new CreateProductResult(Guid.NewGuid());
    }
}
 