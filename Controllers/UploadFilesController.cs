using Microsoft.AspNetCore.Mvc;

namespace McvCoreUtilidades.Controllers;

public class UploadFilesController : Controller
{
    
    private IWebHostEnvironment _hostEnvironment;

    public UploadFilesController(IWebHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
    }
    public IActionResult SubirFile()
    {
        return View();
    }   
    [HttpPost]
    public async Task<IActionResult> SubirFile(IFormFile fichero)
    {
        //NECESITAMOS LA RUTA HACIA wwwroot
        string rootFolder = _hostEnvironment.WebRootPath;
        string fileName = fichero.FileName;
        
        //CUANDO PENSAMOS EN FICHEROS Y SUS RUTAS
        //ESTAMOS PENSANDO EN ALGO PARECIDO A ESTO:
        // C:\\misficheros\carpeta1\1.txt
        //NET CORE NO ES WINDOWS Y ESTA RUTA ES DE WINDOWS.
        //LAS RUTAS DE LINUX PUEDEN DER DISTINTAS Y MACOS
        //DEBEMOS crear rutas con herramientas de Net Core:Path
        
        string path= Path.Combine(rootFolder,"uploads", fileName);
        
        //PARA SUBIR FICHEROS UTLIZAMOS Stream
        using (Stream stream = new FileStream(path, FileMode.Create))
        {
            await fichero.CopyToAsync(stream);
        }

        ViewData["MENSAJE"] = "Fichero subido a " + path;

        ViewData["FILENAME"] = fileName;
        
        return View();
    }
    
    
}