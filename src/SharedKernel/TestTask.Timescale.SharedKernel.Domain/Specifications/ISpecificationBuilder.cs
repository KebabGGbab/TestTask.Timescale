namespace TestTask.Timescale.SharedKernel.Domain.Specifications
{
    public interface ISpecificationBuilder<T>
    {
        ISpecificationBuilder<T> And(ISpecification<T> specification);

        ISpecification<T>? Build();
    }
}
