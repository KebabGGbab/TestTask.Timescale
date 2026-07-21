using System.Linq.Expressions;
using TestTask.Timescale.SharedKernel.Domain.Specifications;

namespace TestTask.Timescale.Domain.Aggregates.MetricsAggregate.Specifications
{
    public class AvgValueMaxSpecification : Specification<Metrics>
    {
        private readonly double _maxAvgValue;

        public AvgValueMaxSpecification(double maxAvgValue)
        {
            _maxAvgValue = maxAvgValue;
        }

        public override Expression<Func<Metrics, bool>> ToExpression()
        {
            return metrics => metrics.AvgValue <= _maxAvgValue;
        }
    }
}
