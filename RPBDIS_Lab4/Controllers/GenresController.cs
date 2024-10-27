using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPBDIS_Lab4.Data;
using RPBDIS_Lab4.Infrastructure.Filters;

namespace RPBDIS_Lab4.Controllers
{
    [TypeFilter(typeof(TimingLogAttribute))]
    public class GenresController(CinemaContext context) : Controller
    {
        private readonly CinemaContext _context = context;
        public async Task<IActionResult> Index()
        {
            return View(await _context.Genres.ToListAsync());
        }
    }
}