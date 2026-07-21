namespace TestTask.Timescale.Application.Queries
{
    public class GetValuesByFileNameAndOrderByDateQuary : IQuery
    {
        public string FileName { get; }

        public GetValuesByFileNameAndOrderByDateQuary(string fileName)
        {
            FileName = fileName;
        }
    }
}
