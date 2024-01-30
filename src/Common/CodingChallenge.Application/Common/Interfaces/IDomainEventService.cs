using System.Threading.Tasks;
using CodingChallenge.Domain.Common;

namespace CodingChallenge.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
