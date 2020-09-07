using System.Collections.Generic;
using MediatR;
using Models;

namespace ApiClient.Queries
{
    public class GetProductsByIdsQuery : IRequest<IEnumerable<Product>>
    {
        public GetProductsByIdsQuery(string[] productIds)
        {
            ProductIds = productIds;
        }

        public string[] ProductIds { get; }
    }
}