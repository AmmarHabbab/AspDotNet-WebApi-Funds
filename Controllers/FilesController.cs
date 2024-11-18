using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles; 
using System.IO;  

namespace WebApi1.Controllers{

[ApiController]
[Authorize]
[Route("api/files")]
public class FilesController : ControllerBase
{
    private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;

    public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
    {
        _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider ?? throw new System.ArgumentNullException(nameof(fileExtensionContentTypeProvider));
    }

    [HttpGet("{fileId}")]
    public ActionResult GetFile(string fileId) 
    {
        var path="bird.jpg";

        if(!System.IO.File.Exists(path))
        {
            return NotFound();
        }

        if(!_fileExtensionContentTypeProvider.TryGetContentType(path,out var contentType))
        {
            contentType = "application/octet-stream"; // catch all for files u dont have specific info about
        }

        var bytes = System.IO.File.ReadAllBytes(path);
        return File(bytes,contentType,Path.GetFileName(path)); //"text/plain" error the file when we open it with show nothing cuz its a photo not a text same goes for pdf
    }

}
}