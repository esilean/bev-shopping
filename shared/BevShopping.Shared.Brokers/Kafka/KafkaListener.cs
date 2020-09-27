using System;
using System.Threading.Tasks;
using BevShopping.Shared.Core.Events;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BevShopping.Shared.Brokers.Kafka
{
    public class KafkaListener : IEventListener
    {
        private readonly IServiceScopeFactory _serviceFactory;
        private readonly KafkaProducerOptions _kafkaProducerOptions;
        private readonly KafkaConsumerOptions _kafkaConsumerOptions;
        private readonly ProducerConfig _producerConfig;
        private readonly ConsumerConfig _consumerConfig;
        private readonly string[] _topics;

        public KafkaListener(
                            IServiceScopeFactory serviceFactory,
                            IOptions<KafkaProducerOptions> producerOptions,
                            IOptions<KafkaConsumerOptions> consumerOptions)
        {
            _kafkaProducerOptions = producerOptions.Value;
            _kafkaConsumerOptions = consumerOptions.Value;
            _serviceFactory = serviceFactory;

            _producerConfig = ConfigProducer();
            _consumerConfig = ConfigConsumer();

            _topics = _kafkaConsumerOptions.Topics.Split(",");
        }

        public async Task Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            using (var p = new ProducerBuilder<string, string>(_producerConfig).Build())
            {
                await p.ProduceAsync(_kafkaProducerOptions.Topic,
                    new Message<string, string>
                    {
                        Key = typeof(TEvent).FullName.ToLower(),
                        Value = JsonConvert.SerializeObject(@event)
                    });
            }
        }

        public void Subscribe(Type type)
        {
            using (var consumer = new ConsumerBuilder<string, string>(_consumerConfig).Build())
            {
                consumer.Subscribe(_topics);
                while (true)
                {

                    var consumerResult = consumer.Consume();
                    if (consumerResult.IsPartitionEOF)
                        continue;

                    var @event = JsonConvert.DeserializeObject(consumerResult.Message.Value, type) as IEvent;

                    using (var scope = _serviceFactory.CreateScope())
                    {
                        var eventBus = scope.ServiceProvider.GetService<IEventBus>();
                        eventBus.PublishLocal(@event);

                        consumer.Commit();
                    }
                }
            }
        }

        public void Subscribe<TEvent>() where TEvent : IEvent
        {
            Subscribe(typeof(TEvent));
        }

        private ConsumerConfig ConfigConsumer()
        {
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = _kafkaConsumerOptions.BootstrapServers,
                GroupId = _kafkaConsumerOptions.GroupId,
                AutoOffsetReset = _kafkaConsumerOptions.AutoOffsetReset,
                EnableAutoCommit = _kafkaConsumerOptions.EnableAutoCommit
            };

            return consumerConfig;
        }

        private ProducerConfig ConfigProducer()
        {
            var producerConfig = new ProducerConfig { BootstrapServers = _kafkaProducerOptions.BootstrapServers };

            //producer safer
            producerConfig.Acks = _kafkaProducerOptions.Acks;
            producerConfig.EnableIdempotence = _kafkaProducerOptions.EnableIdempotence;
            producerConfig.MessageSendMaxRetries = _kafkaProducerOptions.MessageSendMaxRetries;
            producerConfig.MaxInFlight = _kafkaProducerOptions.MaxInFlight;

            //better througput
            producerConfig.CompressionType = _kafkaProducerOptions.CompressionType;
            producerConfig.LingerMs = _kafkaProducerOptions.LingerMs;
            producerConfig.BatchSize = _kafkaProducerOptions.BatchSizeKB * 1024;

            return producerConfig;
        }
    }
}