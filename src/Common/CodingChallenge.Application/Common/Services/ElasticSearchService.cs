using CodingChallenge.Application.Common.Interfaces;
using CodingChallenge.Application.Dto;

using Mapster;
using MapsterMapper;

using Microsoft.EntityFrameworkCore;

using Nest;

using System.Threading.Tasks;

namespace CodingChallenge.Application.Common.Services
{
    /// <summary>
    /// class is responsible for interacting with Elasticsearch, providing methods for indexing individual products and synchronizing the Elasticsearch index with the products stored in the application's database
    /// </summary>
    public class ElasticSearchService : IElasticSearchService
    {
        private readonly IElasticClient _elasticClient;
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _dbContext;

        public ElasticSearchService(IElasticClient elasticClient, IApplicationDbContext dbContext, IMapper mapper)
        {
            _elasticClient = elasticClient;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// creates a indexed product on elastic search container
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task IndexProductAsync(IndexedProductDto product)
        {
            await _elasticClient.CreateDocumentAsync(product);
        }

        /// <summary>
        /// synchronizes the Elasticsearch index with the products stored in the application's database
        /// </summary>
        /// <returns></returns>
        public async Task SyncIndexProductsAsync()
        {
            // Check the count of documents in the index
            var countResponse = await _elasticClient.CountAsync<IndexedProductDto>();

            if (countResponse.Count == 0)
            {
                // Index is empty, perform the sync
                var bulkDescriptor = new BulkDescriptor();

                var products = await _dbContext.Products
                    .Include(x => x.ProductType)
                    .Include(x => x.ProductBrand)
                    .ProjectToType<IndexedProductDto>(_mapper.Config)
                    .ToListAsync();

                foreach (var product in products)
                {
                    // Add each product to the bulk descriptor
                    bulkDescriptor.Index<IndexedProductDto>(op => op
                        .Document(product)
                    );
                }

                await _elasticClient.BulkAsync(bulkDescriptor);
            }
        }
    }
}
