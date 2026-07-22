using System.Collections;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery() : IQuery<GetProductResult>;
    public record GetProductResult(IEnumerable<Product> Products);
    public class GetProductQueryHandler(IDocumentSession session, ILogger<GetProductQueryHandler> logger) : IQueryHandler<GetProductsQuery, GetProductResult>
    {
        public async  Task<GetProductResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductQueryHandler called");
            var products = await session.Query<Product>().ToListAsync(cancellationToken);
            return new GetProductResult(products);
        }
    }
}
