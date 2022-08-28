using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CakeShop.Data;
using CakeShop.Models;

namespace CakeShop.MQ
{
    public class CakeCreateNotification : IConsumer<NewCake>
    {
        private readonly CakesAPPContext _dbContext;

        public CakeCreateNotification(CakesAPPContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ConsumeAsync(NewCake message, CancellationToken cancellationToken)
        {
            var newmessage = new MessageLog
            {
                Message = message.Message,
                TableName = message.TableName
            };
            await _dbContext.AddAsync(newmessage, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
