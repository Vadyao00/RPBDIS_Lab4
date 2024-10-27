using Microsoft.AspNetCore.Mvc;
using RPBDIS_Lab4.Data;
using RPBDIS_Lab4.Infrastructure.Filters;
using RPBDIS_Lab4.Models;
using RPBDIS_Lab4.ViewModels;

namespace RPBDIS_Lab4.Controllers
{
    public class CachedController(CinemaContext context) : Controller
    {
        private readonly CinemaContext _context = context;

        // Кэширование с использования фильтра ресурсов
        [TypeFilter(typeof(CacheResourceFilterAttribute))]
        public IActionResult Index()
        {
            int numberRows = 10;
            List<Actor> actors = [.. _context.Actors.Take(numberRows)];
            List<Genre> genres = [.. _context.Genres.Take(numberRows)];
            List<MovieViewModel> movies = [ .. _context.Movies
                .OrderByDescending(m => m.Duration)
                .Select(t => new MovieViewModel
                {
                    MovieId = t.MovieId,
                    Duration = t.Duration,
                    ProductionCompany = t.ProductionCompany,
                    Country = t.Country,
                    AgeRestriction = t.AgeRestriction,
                    Description = t.Description,
                    Genre = t.Genre.Name,
                })
                .Take(numberRows)];

            HomeViewModel homeViewModel = new()
            {
                Actors = actors,
                Genres = genres,
                Movies = movies
            };
            return View("~/Views/Home/Index.cshtml", homeViewModel);
        }
    }
}