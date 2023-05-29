namespace N5.Challenge.Common.Kafka
{
    public class KafkaMessageDto
    {
        public Guid Id { get; set; }
        public string OperationName { get; set; }
        public string Object { get; set; }
    }
}