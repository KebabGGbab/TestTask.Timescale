using System.Linq.Expressions;

namespace TestTask.Timescale.SharedKernel.Domain.Specifications
{
    public class AndSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> rightExpression = _right.ToExpression();
            Expression<Func<T, bool>> leftExpression = _left.ToExpression();
            ParameterExpression paramExpr = Expression.Parameter(typeof(T));
            BinaryExpression exprBody = Expression.AndAlso(leftExpression.Body, rightExpression.Body);
            exprBody = (BinaryExpression)new ParameterReplacer(paramExpr).Visit(exprBody);
            Expression<Func<T, bool>> finalExpr = Expression.Lambda<Func<T, bool>>(exprBody, paramExpr);

            return finalExpr;
        } 

        public bool IsSatisfiedBy(T candidate)
        {
            return _left.IsSatisfiedBy(candidate) && _right.IsSatisfiedBy(candidate);
        }
    }
}