using TestTask.Timescale.Application.Resources;
using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.Application.Errors
{
    public class FiltersNotAppliedError : Error
    {
        public FiltersNotAppliedError() : base(ErrorMessages.FiltersNotApplied)
        {
        }
    }
}
