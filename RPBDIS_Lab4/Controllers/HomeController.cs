using FuelStation.Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;
using RPBDIS_Lab4.Data;
using RPBDIS_Lab4.Models;
using RPBDIS_Lab4.ViewModels;
using System.Diagnostics;

namespace RPBDIS_Lab4.Controllers
{
    [ExceptionFilter]
    [TypeFilter(typeof(TimingLogAttribute))]
    public class HomeController(CinemaContext db) : Controller
    {
        private readonly CinemaContext _db = db;

        public IActionResult Index()
        {
            int numberRows = 10;
            List<Actor> actors = [.. _db.Actors.Take(numberRows)];
            List<Genre> genres = [.. _db.Genres.Take(numberRows)];
            List<MovieViewModel> movies = [.. _db.Movies
                .OrderByDescending(m => m.Duration)
                .Select(t => new MovieViewModel { MovieId = t.MovieId,
                    Duration = t.Duration,
                    ProductionCompany = t.ProductionCompany,
                    Country = t.Country,
                    AgeRestriction = t.AgeRestriction,
                    Description = t.Description,
                    Genre = t.Genre.Name, })
                .Take(numberRows)];
            HomeViewModel homeViewModel = new() { Actors = actors, Genres = genres, Movies = movies };
            return View(homeViewModel);
        }
    }
}