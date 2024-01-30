using CodingChallenge.Domain.Common;
using CodingChallenge.Domain.Entities;

namespace CodingChallenge.Domain.Event
{
    public class ProductCreatedEvent : DomainEvent
    {
        public ProductCreatedEvent(Product product)
        {
            Product = product;
        }

        public Product Product { get; }
    }
}
