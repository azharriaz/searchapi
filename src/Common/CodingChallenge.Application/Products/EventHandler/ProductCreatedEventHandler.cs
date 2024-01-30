using System.Threading;
using System.Threading.Tasks;

using CodingChallenge.Application.Common.Interfaces;
using CodingChallenge.Application.Common.Models;
using CodingChallenge.Application.Dto;
using CodingChallenge.Domain.Event;

using MapsterMapper;
using MediatR;

using Microsoft.Extensions.Logging;

namespace CodingChallenge.Application.Cities.EventHandler
{
    public class ProductCreatedEventHandler : INotificationHandler<DomainEventNotification<ProductCreatedEvent>>
    {
        private readonly ILogger<ProductCreatedEventHandler> _logger;
        private readonly IElasticSearchService _elasticClientService;
        private readonly IMapper _mapper;

        public ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger, IElasticSearchService elasticClientService, IMapper mapper)
        {
            _logger = logger;
            _elasticClientService = elasticClientService;
            _mapper = mapper;
        }

        public async Task Handle(DomainEventNotification<ProductCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CodingChallenge.Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            if (domainEvent.Product != null)
            {
                var indexedProduct = _mapper.Map<IndexedProductDto>(domainEvent.Product);
                await _elasticClientService.IndexProductAsync(indexedProduct);
            }
        }
    }
}
