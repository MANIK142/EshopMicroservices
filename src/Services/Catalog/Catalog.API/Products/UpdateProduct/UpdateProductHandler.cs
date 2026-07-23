
using FluentValidation;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id,string Name, List<string> Category, String Discription, string ImageFile, decimal Price) :ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandValidator :AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(r => r.Id).NotEmpty().WithMessage("Id cannot be empty");
            RuleFor(r => r.Name).NotEmpty().WithMessage("Name is Requried").Length(2, 150).WithMessage("Name should with minimmum length 2 and maximum length 150");
            RuleFor(r => r.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
    public class UpdateProductHandler(IDocumentSession session, ILogger<UpdateProductHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Update Product request received with payload {@command}", command);

            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
            if(product is null)
            {
                throw new ProductNotFoundException(command.Id);
            }
            product.Name =command.Name;
            product.Description = command.Discription;
            product.Price = command.Price;  
            product.Category = command.Category;
            product.ImageFile = command.ImageFile;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(true);
            
        }
    }
}
