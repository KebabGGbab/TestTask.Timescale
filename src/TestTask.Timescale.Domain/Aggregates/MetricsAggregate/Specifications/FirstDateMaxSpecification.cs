using System.Linq.Expressions;
using TestTask.Timescale.SharedKernel.Domain.Specifications;

namespace TestTask.Timescale.Domain.Aggregates.MetricsAggregate.Specifications
{
    public class FirstDateMaxSpecification : Specification<Metrics>
    {
        private readonly DateTime _maxDate;

        public FirstDateMaxSpecification(DateTime maxDate)
        {
            _maxDate = maxDate;
        }

        public override Expression<Func<Metrics, bool>> ToExpression()
        {
            return metrics => metrics.MinDate <= _maxDate;
        }
    }
}
