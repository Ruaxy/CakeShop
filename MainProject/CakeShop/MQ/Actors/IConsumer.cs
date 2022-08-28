using System.Threading;
using System.Threading.Tasks;

namespace CakeShop.MQ
{
    public interface IConsumer<in T>
    {
        public Task ConsumeAsync(T message, CancellationToken cancellationToken);
    }
}
