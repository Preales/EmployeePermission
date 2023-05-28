namespace N5.Challenge.Common.Kafka
{
    public interface IKafkaService
    {
        Task<bool> SendMessage(KafkaMessageDto kafkaMessageDto);
    }
}