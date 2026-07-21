namespace TestTask.Timescale.Application.Dto
{
    public class MetricsDto
    {
        public int Id { get; }

        public string FileName { get; }

        public double DeltaDate { get; }

        public DateTime MinDate { get; }

        public double AvgExecutionDuration { get; }

        public double AvgValue { get; }

        public double MedianValue { get; }

        public double MaxValue { get; }

        public double MinValue { get; }

        public MetricsDto(int id, string fileName, double deltaDate, DateTime minDate, double avgExecutionDuration,
            double avgValue, double medianValue, double maxValue, double minValue)
        {
            Id = id;
            FileName = fileName;
            DeltaDate = deltaDate;
            MinDate = minDate;
            AvgExecutionDuration = avgExecutionDuration;
            AvgValue = avgValue;
            MedianValue = medianValue;
            MaxValue = maxValue;
            MinValue = minValue;
        }
    }
}
