using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskAssignmentAppNTier.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class FilesController : ControllerBase
  {


    [HttpPost("upload")]
    public async Task<IActionResult> OnPostUploadAsync(IFormFile file)
    {
      

      return Ok();
    }
  }
}
