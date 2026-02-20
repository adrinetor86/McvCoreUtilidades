using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace McvCoreUtilidades.Helpers;


//Enumeraciones con las carpetas que deseemos subir ficheros

public enum Folders {Uploads,Images,Facturas,Temporal,Productos}

public class HelperPathProvider
{

    private IWebHostEnvironment _hostEnvironment;
    private IHttpContextAccessor _httpContext;
    private IServer _server;

    public HelperPathProvider(IWebHostEnvironment hostEnvironment,IHttpContextAccessor httpContext,IServer server)
    {
        _hostEnvironment = hostEnvironment;
        _httpContext = httpContext;
        _server = server;
    }
    
    //TENDREMOS UN METODO QUE SE ENCARGARA DE RESOLVER LA RUTA
    //COMO STRING CUANDO RECIBAMOS EL FICHERO Y LA CARPETA

    public string MapPath(string fileName, Folders folder)
    {
        string carpeta = "";
        if (folder == Folders.Images)
        {
            carpeta = "images";
        }else if (folder == Folders.Uploads)
        {
            carpeta = "uploads";
        }else if (folder == Folders.Facturas)
        {
            carpeta = "facturas";
        }else if (folder == Folders.Temporal)
        {
            carpeta = "temp";
        }else if (folder == Folders.Productos)
        {
            carpeta= Path.Combine("images","productos");
        };

        string rootPath = _hostEnvironment.WebRootPath;
        string path = Path.Combine(rootPath, carpeta, fileName);

        return path;
    }

    public string MapUrlPath(string fileName, Folders folder)
    {
        string carpeta = "";
        if (folder == Folders.Images)
        {
            carpeta = "images";
        }else if (folder == Folders.Uploads)
        {
            carpeta = "uploads";
        }else if (folder == Folders.Facturas)
        {
            carpeta = "facturas";
        }else if (folder == Folders.Temporal)
        {
            carpeta = "temp";
        }else if (folder == Folders.Productos)
        {
            //ESTA SI CAMBIA PORQUE ES SISTEMA DE ARCHIVOS Y
            //NECESITAMOS WEB
            carpeta = "images/productos";

        };
        //LA MANERA DEL ISERVE
        // var addresses = _server.Features.Get<IServerAddressesFeature>().Addresses;
        // string serverUrl = addresses.FirstOrDefault();
        // //DEVOLVEMOS LA RUTA URL
        // string urlPath = serverUrl + "/" + carpeta + "/" + fileName;
        
        //MANERA CON ACCESSOR
        var request = _httpContext.HttpContext.Request;
        string path = $"{request.Scheme}://{request.Host}/{carpeta}/{fileName}";
        return path;
    }
}