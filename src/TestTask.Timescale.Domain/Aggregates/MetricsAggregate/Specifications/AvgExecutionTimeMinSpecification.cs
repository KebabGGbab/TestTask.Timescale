using System.Linq.Expressions;
using TestTask.Timescale.SharedKernel.Domain.Specifications;

namespace TestTask.Timescale.Domain.Aggregates.MetricsAggregate.Specifications
{
    public class AvgExecutionTimeMinSpecification : Specification<Metrics>
    {
        private readonly double _minAvgExecutionTime;

        public AvgExecutionTimeMinSpecification(double minAvgExecutionTime)
        {
            _minAvgExecutionTime = minAvgExecutionTime;
        }

        public override Expression<Func<Metrics, bool>> ToExpression()
        {
            return metrics => metrics.AvgExecutionDuration >= _minAvgExecutionTime;
        }
    }
}
