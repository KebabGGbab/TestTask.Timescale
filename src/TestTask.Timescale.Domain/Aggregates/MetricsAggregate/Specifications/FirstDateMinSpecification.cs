using System.Linq.Expressions;
using TestTask.Timescale.SharedKernel.Domain.Specifications;

namespace TestTask.Timescale.Domain.Aggregates.MetricsAggregate.Specifications
{
    public class FirstDateMinSpecification : Specification<Metrics>
    {
        private readonly DateTime _minDate;

        public FirstDateMinSpecification(DateTime minDate)
        {
            _minDate = minDate;
        }

        public override Expression<Func<Metrics, bool>> ToExpression()
        {
            return metrics => metrics.MinDate >= _minDate; 
        }
    }
}
