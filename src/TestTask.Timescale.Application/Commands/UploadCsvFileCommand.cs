namespace TestTask.Timescale.Application.Commands
{
    public class UploadCsvFileCommand : ICommand
    {
        public Stream Stream { get; }

        public string FileName { get; }

        public UploadCsvFileCommand(Stream stream, string fileName)
        {
            Stream = stream;
            FileName = fileName;
        }
    }
}
