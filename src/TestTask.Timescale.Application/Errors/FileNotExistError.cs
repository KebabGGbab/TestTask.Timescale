using System.Globalization;
using System.Text;
using TestTask.Timescale.Application.Resources;
using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.Application.Errors
{
    public class FileNotExistError : Error
    {
        private static readonly CompositeFormat _message = CompositeFormat.Parse(ErrorMessages.FileNotExistError);

        public FileNotExistError(string fileName)
            : base(string.Format(CultureInfo.InvariantCulture, _message, fileName))
        {
        }
    }
}
