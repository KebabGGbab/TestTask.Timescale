using System.Linq.Expressions;

namespace TestTask.Timescale.SharedKernel.Domain.Specifications
{
    public abstract class Specification<T> : ISpecification<T>
    {
        public abstract Expression<Func<T, bool>> ToExpression();

        public bool IsSatisfiedBy(T candidate)
        {
            return ToExpression().Compile()(candidate);
        }
    }
}
