using System.Threading;
using System.Threading.Tasks;

using CodingChallenge.Application.ApplicationUser.Queries.GetToken;
using CodingChallenge.Application.Common.Models;

using Microsoft.AspNetCore.Mvc;

namespace CodingChallenge.Api.Controllers
{
    /// <summary>
    /// Login controller
    /// </summary>
    public class LoginController : BaseApiController
    {
        /// <summary>
        /// Login and get JWT token email: test@test.com password: CCApi_1850
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ServiceResult<LoginResponse>>> Login(GetTokenQuery query, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }
    }
}
