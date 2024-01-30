using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CodingChallenge.Application.Common.Mapping;
using CodingChallenge.Application.Common.Interfaces;
using CodingChallenge.Application.Common.Models;
using Mapster;
using MapsterMapper;
using CodingChallenge.Application.Dto;

namespace CodingChallenge.Application.Products.Queries.GetProductsWithPagination
{
    public class GetProductsWithPaginationQuery : IRequestWrapper<PaginatedList<ProductDto>>
    {
        public int ProductId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetProductsWithPaginationQueryHandler : IRequestHandlerWrapper<GetProductsWithPaginationQuery, PaginatedList<ProductDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProductsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<PaginatedList<ProductDto>>> Handle(GetProductsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            PaginatedList<ProductDto> list = await _context.Products
                .Where(x => x.Id == request.ProductId)
                .OrderBy(o => o.Name)
                .ProjectToType<ProductDto>(_mapper.Config)
                .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);

            return list.Items.Any() ? ServiceResult.Success(list) : ServiceResult.Failed<PaginatedList<ProductDto>>(ServiceError.NotFound);
        }
    }
}
