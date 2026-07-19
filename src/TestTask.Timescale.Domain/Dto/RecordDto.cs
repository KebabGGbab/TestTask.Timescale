namespace TestTask.Timescale.Domain.Dto
{
    public class RecordDto
    {
        public DateTime Date { get; }

        public double ExecutionTime { get; }

        public double Value { get; }

        public RecordDto(DateTime date, double executionTime, double value)
        {
            Date = date;
            ExecutionTime = executionTime;
            Value = value;
        }
    }
}
