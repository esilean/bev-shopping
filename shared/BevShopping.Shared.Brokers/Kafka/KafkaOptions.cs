using Confluent.Kafka;

namespace BevShopping.Shared.Brokers.Kafka
{
    public class KafkaProducerOptions
    {
        public string BootstrapServers { get; set; }
        public Acks Acks { get; set; }
        public bool EnableIdempotence { get; set; }
        public int MessageSendMaxRetries { get; set; }
        public int MaxInFlight { get; set; }
        public CompressionType CompressionType { get; set; }
        public int LingerMs { get; set; }
        public int BatchSizeKB { get; set; }
        public string Topic { get; set; }
    }

    public class KafkaConsumerOptions
    {
        public string BootstrapServers { get; set; }
        public string GroupId { get; set; }
        public AutoOffsetReset AutoOffsetReset { get; set; }
        public bool EnableAutoCommit { get; set; }
        public string Topics { get; set; }
    }
}