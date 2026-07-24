using System.Collections;
using Marten.Pagination;
namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery(int? PageNumber, int? PageSize) : IQuery<GetProductResult>;
    public record GetProductResult(IEnumerable<Product> Products);
    public class GetProductQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductResult>
    {
        public async  Task<GetProductResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().ToPagedListAsync(request.PageNumber ?? 1,request.PageSize ?? 10, cancellationToken);
            return new GetProductResult(products);
        }
    }
}
