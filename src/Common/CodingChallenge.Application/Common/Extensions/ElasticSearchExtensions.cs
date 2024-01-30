using CodingChallenge.Application.Common.Interfaces;
using CodingChallenge.Application.Common.Services;
using CodingChallenge.Application.Dto;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Nest;

using System;

namespace CodingChallenge.Application.Common.Extensions
{
    /// <summary>
    /// Represents extension class to add elastic search dependencies
    /// </summary>
    public static class ElasticSearchExtensions
    {
        /// <summary>
        /// adds and configures elastic search container dependencies
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddElasticSearch(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var url = configuration["ELKConfiguration:Uri"];
            var defaultIndex = configuration["ELKConfiguration:index"];

            var settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex(defaultIndex)
                .PrettyJson()
                .DisableDirectStreaming();

            var client = new ElasticClient(settings);
            services.AddSingleton<IElasticClient>(client);
            services.AddScoped<IElasticSearchService, ElasticSearchService>();

            CreateIndex(client, defaultIndex);
        }

        /// <summary>
        /// creates default index on elastic search container
        /// </summary>
        /// <param name="client"></param>
        /// <param name="indexName">name of the index to be created</param>
        private static void CreateIndex(IElasticClient client, string indexName)
        {
            client.Indices.Create(indexName, i => i
                    .Map<IndexedProductDto>(x => x
                        .AutoMap()
                        .Properties(p => p
                        .Text(t => t
                            .Name(x => x.Name)
                            .Fielddata(true)) // Enable fielddata for sorting and aggregations
                        .Number(n => n
                            .Name(x => x.Price)
                            .Type(NumberType.Double)) // Assuming Price is a double
                        .Keyword(k => k.Name(x => x.Brand))
                        .Keyword(k => k.Name(x => x.Type))
                        )
                    )
            );
        }
    }
}
