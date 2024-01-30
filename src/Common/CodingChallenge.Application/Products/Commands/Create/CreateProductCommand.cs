using System.Threading;
using System.Threading.Tasks;

using CodingChallenge.Application.Common.Interfaces;
using CodingChallenge.Application.Common.Models;
using CodingChallenge.Application.Dto;

using CodingChallenge.Domain.Entities;
using CodingChallenge.Domain.Event;

using MapsterMapper;

namespace CodingChallenge.Application.Products.Commands.Create
{
    public record CreateProductCommand(string Name, string Description, decimal Price, int BrandId, int TypeId) : IRequestWrapper<ProductDto>;

    public class CreateProductCommandHandler : IRequestHandlerWrapper<CreateProductCommand, ProductDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                ProductBrandId = request.BrandId,
                ProductTypeId = request.TypeId,
            };

            entity.DomainEvents.Add(new ProductCreatedEvent(entity));

            await _context.Products.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<ProductDto>(entity));
        }
    }
}
