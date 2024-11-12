using CrudImage.Core;
using CrudImage.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrudImage.Controller;

[ApiController]
[Route("[controller]")]
public class ImagesController(ImagesService imagesService) : Microsoft.AspNetCore.Mvc.Controller
{
    [HttpGet("GetAllImages")]
    public async Task<IActionResult> GetAllImages()
    {
        return Ok(await imagesService.GetImages());
    }

    [HttpPost("AddImage")]
    public async Task<ActionResult<string>> AddImage(IFormFile file)
    {
        string imageString = await imagesService.ImageUpload(file);

        return Ok(imageString);
    }

    [HttpPut("UpdateImage")]
    public async Task<ActionResult<string>> UpdateImage(int id, IFormFile file)
    {
        return Ok(await imagesService.UpdateImage(id, file));
    }

    [HttpDelete("DeleteImage")]
    public async Task<ActionResult<string>> DeleteImage(int id)
    {
        return Ok(await imagesService.DeleteImage(id));
    }
}