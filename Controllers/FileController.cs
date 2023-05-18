using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Diagnostics;

namespace AspCoreWebAPIDemos.Controllers
{
    [Route("api/download")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider? _fileExtensionContentTypeProvider;

        public FileController(FileExtensionContentTypeProvider? fileExtensionContentTypeProvider)
        {
            _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider
                ?? throw new ArgumentNullException(nameof(fileExtensionContentTypeProvider));
        }

        [HttpGet("{fileId}")]
        public ActionResult GetFile(int fileId)
        {
            const string pathToFile = "./sample.pdf";

            if (!System.IO.File.Exists(pathToFile))
            {
                return NotFound("File not found");
            }

            if (!_fileExtensionContentTypeProvider!.TryGetContentType(pathToFile, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var allBytes = System.IO.File.ReadAllBytes(pathToFile);
            return File(allBytes, contentType, Path.GetFileName(pathToFile));

        }
    }
}
