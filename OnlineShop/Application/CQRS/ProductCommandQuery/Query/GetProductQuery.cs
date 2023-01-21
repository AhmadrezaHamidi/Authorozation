using AutoMapper;
using Core.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.ProductCommandQuery.Query
{
    public class GetProductQuery:IRequest<GetProductQueryResponse>
    {
        public int Id { get; set; }
    }

    public class GetProductQueryResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PriceWithComma { get; set; }
    }

    public class ProductQueryHandler : IRequestHandler<GetProductQuery, GetProductQueryResponse>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }
        public async Task<GetProductQueryResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetAsync(request.Id);
            var response = mapper.Map<GetProductQueryResponse>(product);
            return response;
        }
    }
}
