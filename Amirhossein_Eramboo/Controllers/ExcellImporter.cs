using Application;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Amirhossein_Eramboo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExcellImporter : ControllerBase
    {
        private readonly ExcellImporterAppService _appService;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public ExcellImporter(IBackgroundJobClient backgroundJobClient,
            ExcellImporterAppService appService)
        {
            _backgroundJobClient = backgroundJobClient;
            _appService = appService;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var extension = Path.GetExtension(file.FileName);
            if (extension != ".xls" && extension != ".xlsx")
            {
                return BadRequest("Please upload a valid Excel file.");
            }


            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0; // Reset stream position

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();

                    _backgroundJobClient.Enqueue(() =>_appService.SaveExcel(fileBytes, file.FileName));
                }
            }

            return Ok("File uploaded and processed successfully.");
        }
    }
}
