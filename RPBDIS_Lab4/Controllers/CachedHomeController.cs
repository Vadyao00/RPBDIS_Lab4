using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RPBDIS_Lab4.ViewModels;

namespace RPBDIS_Lab4.Controllers
{
    public class CachedHomeController(IMemoryCache memoryCache) : Controller
    {
        private readonly IMemoryCache _memoryCache = memoryCache;
        public IActionResult Index()
        {
            //считывание данных из кэша
            HomeViewModel homeViewModel = _memoryCache.Get<HomeViewModel>("Movies 10");
            return View("~/Views/Home/Index.cshtml", homeViewModel);
        }
    }
}