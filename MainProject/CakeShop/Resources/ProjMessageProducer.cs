using ActiveMQ.Artemis.Client;
using System.Threading.Tasks;

namespace CakeShop.Resources
{
    public class ProjMessageProducer
    {
        private readonly IProducer _producer;

        public ProjMessageProducer(IProducer producer)
        {
            _producer = producer;
        }

        public async Task SendTextAsync(string text)
        {
            var message = new Message(text);
            await _producer.SendAsync(message);
        }
    }
}
