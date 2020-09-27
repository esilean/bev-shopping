using BevShopping.Shared.Core.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BevShopping.Shared.Brokers.Kafka
{
    public static class KafkaExtensions
    {
        public static IServiceCollection AddKafka(this IServiceCollection services, IConfiguration Configuration)
        {

            services.Configure<KafkaProducerOptions>(Configuration.GetSection("KafkaOptions:KafkaProducer"));
            services.Configure<KafkaConsumerOptions>(Configuration.GetSection("KafkaOptions:KafkaConsumer"));

            services.AddSingleton<IEventListener, KafkaListener>();

            return services;
        }

        public static IApplicationBuilder UseKafkaSubscribe<T>(this IApplicationBuilder app) where T : IEvent
        {
            app.ApplicationServices.GetRequiredService<IEventListener>().Subscribe<T>();

            return app;
        }
    }
}