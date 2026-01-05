using System.Diagnostics;
using DocumentConverter.Models;
using Microsoft.AspNetCore.Mvc;

namespace DocumentConverter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Convert()
        {
            return View(new FileConversionModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Convert(FileConversionModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.File == null || model.File.Length == 0)
                return BadRequest("No file uploaded");

            var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");
            var convertedDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Converted");

            Directory.CreateDirectory(uploadsDir);
            Directory.CreateDirectory(convertedDir);

            var inputFilePath = Path.Combine(uploadsDir, model.File.FileName);

            await using (var fs = new FileStream(inputFilePath, FileMode.Create))
            {
                await model.File.CopyToAsync(fs);
            }

            string outputExtension;
            string convertFilter;

            if (model.ConversionType == ConversionType.DocxToPdf)
            {
                outputExtension = ".pdf";
                convertFilter = "pdf:writer_pdf_Export";
            }
            else if (model.ConversionType == ConversionType.PdfToDocx)
            {
                outputExtension = ".docx";
                convertFilter = "docx";
            }
            else
            {
                return BadRequest("Invalid conversion type");
            }

            var args =
                $"--headless --nologo --nodefault --nolockcheck --nofirststartwizard " +
                $"--convert-to {convertFilter} " +
                $"\"{inputFilePath}\" --outdir \"{convertedDir}\"";

            var psi = new ProcessStartInfo
            {
                FileName = "soffice",
                Arguments = args,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi);
            if (process == null)
                return StatusCode(500, "Failed to start LibreOffice");

            await process.WaitForExitAsync();

            var outputFilePath = Path.Combine(
                convertedDir,
                Path.GetFileNameWithoutExtension(model.File.FileName) + outputExtension);

            if (!System.IO.File.Exists(outputFilePath))
            {
                var error = await process.StandardError.ReadToEndAsync();
                return StatusCode(500, $"Conversion failed: {error}");
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(outputFilePath);

            var contentType = model.ConversionType == ConversionType.DocxToPdf
                ? "application/pdf"
                : "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

            return File(bytes, contentType, Path.GetFileName(outputFilePath));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
