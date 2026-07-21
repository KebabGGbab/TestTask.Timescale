using System.Linq.Expressions;
using TestTask.Timescale.SharedKernel.Domain.Specifications;

namespace TestTask.Timescale.Domain.Aggregates.MetricsAggregate.Specifications
{
    public class AvgExecutionTimeMaxSpecification : Specification<Metrics>
    {
        private readonly double _maxAvgExecutionTime;

        public AvgExecutionTimeMaxSpecification(double maxAvgExecutionTime)
        {
            _maxAvgExecutionTime = maxAvgExecutionTime;
        }

        public override Expression<Func<Metrics, bool>> ToExpression()
        {
            return metrics => metrics.AvgExecutionDuration >= _maxAvgExecutionTime;
        }
    }
}