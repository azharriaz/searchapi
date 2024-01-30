using CodingChallenge.Application.Common.Interfaces;
using CodingChallenge.Application.Common.Models;
using CodingChallenge.Application.Common.Extensions;
using CodingChallenge.Application.Dto;

using Nest;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodingChallenge.Application.Products.Queries.Search
{
    /// <summary>
    /// class represents a query object used for searching products
    /// </summary>
    public class SearchProductsQuery : IRequestWrapper<List<IndexedProductDto>>
    {
        public string Query { get; set; }
        public List<SearchCriteria> Criterias { get; set; } = new List<SearchCriteria>();
        public List<SortingCriteria> Sorting { get; set; } = new List<SortingCriteria>();
        public int PageSize { get; set; } = 10;
    }

    /// <summary>
    /// class is a handler responsible for processing and executing search queries on products
    /// </summary>
    public class SearchProductsQueryHandler : IRequestHandlerWrapper<SearchProductsQuery, List<IndexedProductDto>>
    {
        private readonly IElasticClient _elasticClient;

        public SearchProductsQueryHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<ServiceResult<List<IndexedProductDto>>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
        {
            var queryContainer = new List<QueryContainer>();

            foreach (var criteria in request.Criterias)
            {
                var fieldName = criteria.Param.ToLower();

                queryContainer = criteria.Conjunction?.ToLower() switch
                {
                    "or" => queryContainer.Equal(fieldName, criteria.Value),
                    "and" => queryContainer.Equal(fieldName, criteria.Value),
                    "lt" => queryContainer.Lt(fieldName, double.Parse(criteria.Value)),
                    "gt" => queryContainer.Gt(fieldName, double.Parse(criteria.Value)),
                    _ => queryContainer.Equal(fieldName, criteria.Value)
                };
            }

            queryContainer.QueryString(request.Query, new string[] { "name", "description", "brand", "type" });
            var boolQuery = new BoolQuery()
            {
                Filter = queryContainer.ToArray(),
            };

            var searchDescriptor = new SearchDescriptor<IndexedProductDto>()
            .Query(q => boolQuery)
            .Size(request.PageSize);

            foreach (var sorting in request.Sorting)
            {
                var fieldName = sorting.Field.ToLower();
                var sortOrder = sorting.Order.ToLower();

                searchDescriptor = sortOrder switch
                {
                    "asc" => searchDescriptor.Sort(s => s.Ascending(fieldName)),
                    "desc" => searchDescriptor.Sort(s => s.Descending(fieldName)),
                    _ => searchDescriptor
                };
            }

            var results = await _elasticClient.SearchAsync<IndexedProductDto>(searchDescriptor);

            return results.Total > 0
                ? ServiceResult.Success(results.Documents.ToList())
                : ServiceResult.Failed<List<IndexedProductDto>>(ServiceError.NotFound);
        }
    }

    public class SearchCriteria
    {
        public string Param { get; set; }
        public string Value { get; set; }
        public string Conjunction { get; set; }
    }

    public class SortingCriteria
    {
        public string Field { get; set; }
        public string Order { get; set; }
    }
}
