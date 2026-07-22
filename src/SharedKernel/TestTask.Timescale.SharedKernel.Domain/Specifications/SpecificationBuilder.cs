namespace TestTask.Timescale.SharedKernel.Domain.Specifications
{
    public class SpecificationBuilder<T> : ISpecificationBuilder<T>
    {
        private ISpecification<T>? _currentSpecification;

        public ISpecificationBuilder<T> And(ISpecification<T> specification)
        {
            _currentSpecification = _currentSpecification == null
                ? specification
                : _currentSpecification.And(specification);

            return this;
        }

        public ISpecification<T>? Build()
        {
            return _currentSpecification;
        }
    }
}
