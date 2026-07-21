using System.Linq.Expressions;

namespace TestTask.Timescale.SharedKernel.Domain.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> ToExpression();

        bool IsSatisfiedBy(T candidate);
    }
}
