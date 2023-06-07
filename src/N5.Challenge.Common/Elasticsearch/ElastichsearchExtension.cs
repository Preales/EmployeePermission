using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace N5.Challenge.Common.Elasticsearch
{
    public static class ElastichsearchExtension
    {
        public static IServiceCollection AddElastichsearchService(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<ElasticsearchSetting>(option => config.GetSection("Elasticsearch").Bind(option));
            services.AddScoped<IElasticsearchService, ElasticsearchService>();
            return services;
        }
    }
}
