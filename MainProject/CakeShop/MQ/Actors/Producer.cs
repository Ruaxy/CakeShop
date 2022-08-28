using System.Text.Json;
using System.Threading.Tasks;
using ActiveMQ.Artemis.Client;


namespace CakeShop.MQ
{
    public class Producer
    {
        private readonly IAnonymousProducer _producer;

        public Producer(IAnonymousProducer producer)
        {
            _producer = producer;
        }

        public async Task PublishAsync<T>(T message)
        {
            var serialized = JsonSerializer.Serialize(message);
            var address = typeof(T).Name;
            var msg = new Message(serialized);
            await _producer.SendAsync(address, msg);
        }
    }
}
