using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace N5.Challenge.Common.Kafka
{
    public class KafkaService : IKafkaService
    {
        private readonly KafkaSetting _kafkaSetting;

        public KafkaService(
            IOptions<KafkaSetting> kafkaSetting)
        {
            _kafkaSetting = kafkaSetting.Value;
        }

        public async Task<bool> SendMessage(KafkaMessageDto kafkaMessageDto)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = $"{_kafkaSetting.HostName}:{_kafkaSetting.Port}"
            };

            using (var producer = new ProducerBuilder<Guid, string>(config).Build())
            {
                string value = JsonSerializer.Serialize(kafkaMessageDto);
                var message = new Confluent.Kafka.Message<Guid, string> { Key = Guid.NewGuid(), Value = value };

                var deliveryResult = await producer.ProduceAsync(kafkaMessageDto.OperationName, message);
                return deliveryResult.Status == PersistenceStatus.Persisted;
            }
        }
    }
}