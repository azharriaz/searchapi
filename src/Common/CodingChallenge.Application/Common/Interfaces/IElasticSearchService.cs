using CodingChallenge.Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingChallenge.Application.Common.Interfaces
{
    public interface IElasticSearchService
    {
        Task IndexProductAsync(IndexedProductDto product);

        Task SyncIndexProductsAsync();
    }
}
