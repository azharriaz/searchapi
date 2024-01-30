using CodingChallenge.Application.Products.Queries.GetProductsWithPagination;
using CodingChallenge.Application.Products.Queries.Search;
using CodingChallenge.Application.Dto;
using CodingChallenge.Application.Products.Commands.Create;
using CodingChallenge.Application.Common.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using System.Threading.Tasks;
using System.Threading;


namespace CodingChallenge.Api.Controllers
{
    /// <summary>
    /// Products controller
    /// </summary>
    [Authorize]
    public class ProductsController : BaseApiController
    {
        /// <summary>
        /// Get all products with pagination
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ServiceResult<PaginatedList<ProductDto>>>> GetProductsWithPagination([FromQuery] GetProductsWithPaginationQuery query, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        /// <summary>
        /// Get all products with pagination
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("Search")]
        public async Task<ActionResult> GetProductsByQuery(SearchProductsQuery query,CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        /// <summary>
        /// Creates Product
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ServiceResult<ProductDto>>> Create(CreateProductCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }
    }
}
