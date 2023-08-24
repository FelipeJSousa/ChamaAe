using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ChamaAe.Servico.Infra.Data.Extensions
{
    public class MyExecutionStrategy : ExecutionStrategy
    {
        public MyExecutionStrategy(DbContext context) : this(context, 3, TimeSpan.FromSeconds(3))
        {
        }

        public MyExecutionStrategy(DbContext context, int maxRetryCount, TimeSpan maxRetryDelay) : base(context, maxRetryCount, maxRetryDelay)
        {
        }

        public MyExecutionStrategy(ExecutionStrategyDependencies dependencies) : this(dependencies, 3, TimeSpan.FromSeconds(3))
        {
        }

        public MyExecutionStrategy(ExecutionStrategyDependencies dependencies, int maxRetryCount, TimeSpan maxRetryDelay) : base(dependencies, maxRetryCount, maxRetryDelay)
        {
        }

        protected override bool ShouldRetryOn(Exception exception)
        {
            return true;
        }
    }
}