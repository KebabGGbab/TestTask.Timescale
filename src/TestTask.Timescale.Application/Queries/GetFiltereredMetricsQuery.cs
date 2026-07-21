namespace TestTask.Timescale.Application.Queries
{
    public class GetFiltereredMetricsQuery : IQuery
    {
        public string? FileName { get; }

        public DateTime? FirstDateMin { get; }

        public DateTime? FirstDateMax { get; }

        public double? AvgExecutionTimeMin { get; }

        public double? AvgExecutionTimeMax { get; }

        public double? AvgValueMin { get; }

        public double? AvgValueMax { get; }

        public GetFiltereredMetricsQuery(string? fileName, DateTime? firstDateMin, DateTime? firstDateMax,
            double? avgExecutionTimeMin, double? avgExecutionTimeMax, double? avgValueMin, double? avgValueMax)
        {
            FileName = fileName;
            FirstDateMin = firstDateMin;
            FirstDateMax = firstDateMax;
            AvgExecutionTimeMin = avgExecutionTimeMin;
            AvgExecutionTimeMax = avgExecutionTimeMax;
            AvgValueMin = avgValueMin;
            AvgValueMax = avgValueMax;
        }
    }
}
