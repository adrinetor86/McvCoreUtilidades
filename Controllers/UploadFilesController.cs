using McvCoreUtilidades.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace McvCoreUtilidades.Controllers;

public class UploadFilesController : Controller
{

    private HelperPathProvider _helperPath;
    
    public UploadFilesController(HelperPathProvider helperPath)
    {
        _helperPath = helperPath;
    }
    public IActionResult SubirFile()
    {
        return View();
    }   
    [HttpPost]
    public async Task<IActionResult> SubirFile(IFormFile fichero)
    {
 
        string fileName = fichero.FileName;

        string path = _helperPath.MapPath(fileName, Folders.Productos);
        
        //PARA SUBIR FICHEROS UTLIZAMOS Stream
        using (Stream stream = new FileStream(path, FileMode.Create))
        {
            await fichero.CopyToAsync(stream);
        }

        ViewData["MENSAJE"] = "Fichero subido a " + path;

        ViewData["PATH"] = _helperPath.MapUrlPath(fileName,Folders.Productos);
        
        return View();
    }
    
    
}