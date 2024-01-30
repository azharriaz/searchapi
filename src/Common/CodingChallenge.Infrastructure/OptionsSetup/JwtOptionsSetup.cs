using CodingChallenge.Domain.Authentication;
using CodingChallenge.Domain.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CodingChallenge.Infrastructure.OptionsSetup
{
    public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
    {
        private readonly IConfiguration _configuration;

        public JwtOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(JwtOptions options)
        {
            _configuration.GetSection(Constants.JWTSectionName).Bind(options);
        }
    }
}

