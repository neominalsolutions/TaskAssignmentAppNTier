using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;
using System.Security.Cryptography.Xml;

namespace TaskAssignmentAppNTier.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class FilesController : ControllerBase
  {

    class FileResult
    {
      public string Base64 { get; set; }

    }

    [HttpPost("upload")]
    public async Task<IActionResult> OnPostUploadAsync(IFormFile file)
    {

      var filePath = Path.GetTempFileName();

      using (var stream = new MemoryStream())
      {
        file.CopyTo(stream);
        var bytes = stream.ToArray();

        var result = new FileResult();
        result.Base64 = $"data:{file.ContentType};base64,{Convert.ToBase64String(bytes)}";
       

        return Ok(
          result
          );

      }

    }
  }
}
