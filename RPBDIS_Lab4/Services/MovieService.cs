using RPBDIS_Lab4.Data;
using RPBDIS_Lab4.ViewModels;

namespace RPBDIS_Lab4.Services
{
    public class MovieService(CinemaContext db) : IMovieService
    {
        private readonly CinemaContext _context = db;

        public HomeViewModel GetHomeViewModel(int numberRows = 10)
        {
            var actors = _context.Actors.Take(numberRows).ToList();
            var genres = _context.Genres.Take(numberRows).ToList();
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
            return homeViewModel;
        }
    }
}