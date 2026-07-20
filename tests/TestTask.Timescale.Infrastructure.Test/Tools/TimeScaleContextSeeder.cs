using Microsoft.EntityFrameworkCore;
using TestTask.Timescale.Domain.Aggregates.MetricsAggregate;
using TestTask.Timescale.Domain.Aggregates.RecordAggregate;
using TestTask.Timescale.Domain.Aggregates.TimeScaleAggregate;
using TestTask.Timescale.Domain.Dto;

namespace TestTask.Timescale.Infrastructure.Test.Tools
{
    internal class TimeScaleContextSeeder
    {
        public static async Task SeedAsync(DbContext context, bool _, CancellationToken cancellation = default)
        {
            List<List<RecordDto>> dtos = GetRecordDtos();

            for (int i = 0; i < dtos.Count; ++i)
            {
                TimeScale scale = TimeScale.Create(dtos[i], $"file{i+1}.csv").Value;

                context.Set<TimeScale>().Add(scale);
                await context.SaveChangesAsync(cancellation);

                List<Record> records = new(dtos.Count);

                foreach (RecordDto dto in dtos[i])
                {
                    records.Add(Record.Create(scale.Id, dto).Value);
                }

                context.Set<Record>().AddRange(records);

                Metrics metrics = Metrics.Create(scale.Id, dtos[i]).Value;
                context.Set<Metrics>().Add(metrics);

                await context.SaveChangesAsync(cancellation);
            }
        }

        public static void Seed(DbContext context, bool _)
        {
            List<List<RecordDto>> dtos = GetRecordDtos();

            for (int i = 0; i < dtos.Count; ++i)
            {

                TimeScale scale = TimeScale.Create(dtos[i], $"file{i+1}.csv").Value;

                context.Set<TimeScale>().Add(scale);
                context.SaveChanges();

                List<Record> records = new(dtos[i].Count);

                foreach (RecordDto dto in dtos[i])
                {
                    records.Add(Record.Create(scale.Id, dto).Value);
                }

                context.Set<Record>().AddRange(records);
                Metrics metrics = Metrics.Create(scale.Id, dtos[i]).Value;
                context.Set<Metrics>().Add(metrics);
                context.SaveChanges();
            }
        }

        private static List<List<RecordDto>> GetRecordDtos()
        {
            return [
                [
                    new RecordDto(new DateTime(2026, 05, 20, 14, 00, 00, 000, DateTimeKind.Utc), 10.150, 101.3),
                    new RecordDto(new DateTime(2026, 05, 20, 14, 00, 10, 150, DateTimeKind.Utc), 12.350, 102.5),
                    new RecordDto(new DateTime(2026, 05, 20, 14, 00, 22, 500, DateTimeKind.Utc), 15.100, 100.8),
                    new RecordDto(new DateTime(2026, 05, 20, 14, 00, 37, 600, DateTimeKind.Utc), 08.400, 104.2),
                    new RecordDto(new DateTime(2026, 05, 20, 14, 00, 46, 000, DateTimeKind.Utc), 14.250, 103.9),
                    new RecordDto(new DateTime(2026, 05, 20, 14, 01, 00, 250, DateTimeKind.Utc), 11.050, 105.1),
                    new RecordDto(new DateTime(2026, 05, 20, 14, 01, 11, 300, DateTimeKind.Utc), 19.700, 106.4),
                    new RecordDto(new DateTime(2026, 05, 20, 14, 01, 31, 000, DateTimeKind.Utc), 05.125, 104.7),
                    new RecordDto(new DateTime(2026, 05, 20, 14, 01, 36, 125, DateTimeKind.Utc), 13.375, 107.2),
                    new RecordDto(new DateTime(2026, 05, 20, 14, 01, 49, 500, DateTimeKind.Utc), 10.500, 108.0)
                ],
                [
                    new RecordDto(new DateTime(2024, 11, 12, 06, 30, 00, 000, DateTimeKind.Utc), 15.450, 550.1),
                    new RecordDto(new DateTime(2024, 11, 12, 06, 30, 15, 450, DateTimeKind.Utc), 08.150, 552.4),
                    new RecordDto(new DateTime(2024, 11, 12, 06, 30, 23, 600, DateTimeKind.Utc), 12.400, 549.8),
                    new RecordDto(new DateTime(2024, 11, 12, 06, 30, 36, 000, DateTimeKind.Utc), 20.300, 553.2),
                    new RecordDto(new DateTime(2024, 11, 12, 06, 30, 56, 300, DateTimeKind.Utc), 05.250, 551.7),
                    new RecordDto(new DateTime(2024, 11, 12, 06, 31, 01, 550, DateTimeKind.Utc), 14.100, 555.0),
                    new RecordDto(new DateTime(2024, 11, 12, 06, 31, 15, 650, DateTimeKind.Utc), 11.350, 554.3)
                ],
                [
                    new RecordDto(new DateTime(2025, 08, 04, 19, 15, 00, 000, DateTimeKind.Utc), 14.250, 710.2),
                    new RecordDto(new DateTime(2025, 08, 04, 19, 15, 14, 250, DateTimeKind.Utc), 09.350, 712.5),
                    new RecordDto(new DateTime(2025, 08, 04, 19, 15, 23, 600, DateTimeKind.Utc), 18.150, 711.9),
                    new RecordDto(new DateTime(2025, 08, 04, 19, 15, 41, 750, DateTimeKind.Utc), 05.250, 714.3),
                    new RecordDto(new DateTime(2025, 08, 04, 19, 15, 47, 000, DateTimeKind.Utc), 12.400, 713.0),
                    new RecordDto(new DateTime(2025, 08, 04, 19, 15, 59, 400, DateTimeKind.Utc), 15.100, 715.8),
                    new RecordDto(new DateTime(2025, 08, 04, 19, 16, 14, 500, DateTimeKind.Utc), 08.500, 714.6),
                    new RecordDto(new DateTime(2025, 08, 04, 19, 16, 23, 000, DateTimeKind.Utc), 22.350, 717.1),
                    new RecordDto(new DateTime(2025, 08, 04, 19, 16, 45, 350, DateTimeKind.Utc), 11.150, 716.4),
                    new RecordDto(new DateTime(2025, 08, 04, 19, 16, 56, 500, DateTimeKind.Utc), 06.500, 718.9),
                    new RecordDto(new DateTime(2025, 08, 04, 19, 17, 03, 000, DateTimeKind.Utc), 14.800, 717.5),
                    new RecordDto(new DateTime(2025, 08, 04, 19, 17, 17, 800, DateTimeKind.Utc), 10.200, 719.3)
                ]
            ];
        }
    }
}
