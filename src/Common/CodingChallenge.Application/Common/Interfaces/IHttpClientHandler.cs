using System.Threading;
using System.Threading.Tasks;
using CodingChallenge.Application.Common.Models;
using CodingChallenge.Domain.Enums;

namespace CodingChallenge.Application.Common.Interfaces
{
    public interface IHttpClientHandler
    {
        Task<ServiceResult<TResult>> GenericRequest<TRequest, TResult>(string clientApi, string url,
            CancellationToken cancellationToken,
            MethodType method = MethodType.Get,
            TRequest requestEntity = null)
            where TResult : class where TRequest : class;
    }
}