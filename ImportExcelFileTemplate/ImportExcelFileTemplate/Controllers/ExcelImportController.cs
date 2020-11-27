using System.Threading.Tasks;
using ImportExcelFileTemplate.Helpers;
using ImportExcelFileTemplate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ImportExcelFileTemplate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExcelImportController : ControllerBase
    {
        private readonly ILogger<ExcelImportController> _logger;

        public ExcelImportController(ILogger<ExcelImportController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload()
        {
            if (HttpContext.Request.Form.Files[0] != null)
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                var file = HttpContext.Request.Form.Files[0];

                var stream = file.OpenReadStream();

                var importedData = FileReader.ExtractDataFromStream(stream);

                return Ok(importedData);
            }

            return BadRequest("Please check the format of your file and try again");
        }
    }
}
