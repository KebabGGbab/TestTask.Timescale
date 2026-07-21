using Microsoft.AspNetCore.Mvc;
using TestTask.Timescale.Application.Commands;
using TestTask.Timescale.Application.Dto;
using TestTask.Timescale.Application.Queries;
using TestTask.Timescale.Domain.Dto;
using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TimeScaleController : ControllerBase
    {
        private readonly string[] permittedExtensions = [".csv"];

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload(IFormFile file, ICommandHandler<UploadCsvFileCommand, Result> command)
        {
            string fileName = Path.GetFileName(file.FileName).ToLowerInvariant();
            string extension = Path.GetExtension(fileName);

            if (string.IsNullOrEmpty(fileName) || !permittedExtensions.Contains(extension))
            {
                return BadRequest("The file must have the extension \".csv\".");
            }

            using Stream stream = file.OpenReadStream();
            await command.HandleAsync(new UploadCsvFileCommand(stream, fileName));

            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Metrics(GetFiltereredMetricsQuery query, IQueryHandler<GetFiltereredMetricsQuery, Result<IEnumerable<MetricsDto>>> handler)
        {
            Result<IEnumerable<MetricsDto>> result = await handler.HandleAsync(query); 

            if (result.IsFailure)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> Values(string fileName, IQueryHandler<GetValuesByFileNameAndOrderByDateQuary, Result<IEnumerable<RecordDto>>> query)
        {
            Result<IEnumerable<RecordDto>> result = await query.HandleAsync(new GetValuesByFileNameAndOrderByDateQuary(fileName));

            if (result.IsFailure)
            {
                return NotFound(result.Errors);
            }

            return Ok(result.Value);
        }
    }
}
