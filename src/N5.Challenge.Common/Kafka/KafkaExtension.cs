using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace N5.Challenge.Common.Kafka
{
    public static class KafkaExtension
    {
        public static IServiceCollection AddKafkaService(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<KafkaSetting>(option => config.GetSection("KafkaSetting").Bind(option));
            services.AddScoped<IKafkaService, KafkaService>();
            return services;
        }
    }
}