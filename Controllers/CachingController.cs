using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace McvCoreUtilidades.Controllers;

public class CachingController : Controller
{
    private IMemoryCache _memoryCache;
    public CachingController(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }
    // GET
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult MemoriaPersonalizada(int? tiempo)
    {
        if (tiempo == null)
        {
            tiempo = 60;
        }
        string fecha = DateTime.Now.ToLongDateString() + " -- " +
                       DateTime.Now.ToLongTimeString();
        
        //COMO ESTO ES MANUAL, DEBEMOS PREGUNTAR SI EXISTE
        //ALGO EN CACHE O NO

        if (_memoryCache.Get("FECHA") == null)
        {
            //NO EXISTE CACHE TODAVIA
            //CREAMOS EL OBJETO ENTRY OPTIONS CON EL TIEMPO
            MemoryCacheEntryOptions options =
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(tiempo.Value));
            
            _memoryCache.Set("FECHA", fecha,options);
            ViewData["MENSAJE"] = "Fecha almacenada correctamente";
            ViewData["FECHA"] = _memoryCache.Get("FECHA");
        }
        else
        {
            //EXISTE CACHE Y LO RECUPERAMOS
            fecha = _memoryCache.Get<string>("FECHA");
            ViewData["MENSAJE"] = "Fecha recuperada correctamente";
            ViewData["FECHA"] = fecha;
        }
        return View();
    }

    [ResponseCache(Duration=60,
        Location = ResponseCacheLocation.Client)]
    public IActionResult MemoriaDistribuida()
    {
        string fecha = DateTime.Now.ToLongDateString() + " -- " +
                       DateTime.Now.ToLongTimeString();
        ViewData["FECHA"] = fecha;
        return View();
    }
}