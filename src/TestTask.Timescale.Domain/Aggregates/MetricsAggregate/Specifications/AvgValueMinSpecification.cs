using System.Linq.Expressions;
using TestTask.Timescale.SharedKernel.Domain.Specifications;

namespace TestTask.Timescale.Domain.Aggregates.MetricsAggregate.Specifications
{
    public class AvgValueMinSpecification : Specification<Metrics>
    {
        private readonly double _minAvgValue;

        public AvgValueMinSpecification(double minAvgValue)
        {
            _minAvgValue = minAvgValue;
        }

        public override Expression<Func<Metrics, bool>> ToExpression()
        {
            return metrics => metrics.AvgValue >= _minAvgValue;
        }
    }
}
