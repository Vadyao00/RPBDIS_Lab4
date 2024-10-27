using RPBDIS_Lab4.Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPBDIS_Lab4.Data;
using RPBDIS_Lab4.Infrastructure;
using RPBDIS_Lab4.ViewModels;
using RPBDIS_Lab4.Models;

namespace RPBDIS_Lab4.Controllers
{
    [TypeFilter(typeof(TimingLogAttribute))] // Фильтр ресурсов
    [ExceptionFilter] // Фильтр исключений
    public class MoviesController(CinemaContext context) : Controller
    {
        private readonly int pageSize = 10;   // количество элементов на странице
        private readonly CinemaContext _context = context;
        private MovieViewModel _movie = new()
        {
            Genre = ""
        };

        // GET: Movies
        [SetToSession("SortState")] //Фильтр действий для сохранение в сессию состояния сортировки
        public IActionResult Index(SortState sortOrder, int page = 1)
        {
            // Считывание данных из сессии
            var sessionMovie = Infrastructure.SessionExtensions.Get(HttpContext.Session, "movie");
            var sessionSortState = Infrastructure.SessionExtensions.Get(HttpContext.Session, "SortState");
            if (sessionMovie != null)
                _movie = Transformations.DictionaryToObject<MovieViewModel>(sessionMovie);
            if ((sessionSortState != null))
                if ((sessionSortState.Count > 0) & (sortOrder == SortState.No)) sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState["sortOrder"]);

            // Сортировка и фильтрация данных
            IQueryable<Movie> cinemaContext = _context.Movies;
            cinemaContext = Sort_Search(cinemaContext, sortOrder, _movie.Genre ?? "");

            // Разбиение на страницы
            var count = cinemaContext.Count();
            cinemaContext = cinemaContext.Skip((page - 1) * pageSize).Take(pageSize);

            // Формирование модели для передачи представлению
            _movie.SortViewModel = new SortViewModel(sortOrder);
            MoviesViewModel operations = new()
            {
                Movies = cinemaContext,
                PageViewModel = new PageViewModel(count, page, pageSize),
                MovieViewModel = _movie
            };
            return View(operations);
        }
        // Post: Movies
        [HttpPost]
        [SetToSession("Movie")] //Фильтр действий для сохранение в сессию параметров отбора
        public IActionResult Index(MovieViewModel movie, int page = 1)
        {
            // Считывание данных из сессии
            var sessionSortState = Infrastructure.SessionExtensions.Get(HttpContext.Session, "SortState");
            var sortOrder = new SortState();
            if (sessionSortState.Count > 0)
                sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState["sortOrder"]);

            // Сортировка и фильтрация данных
            IQueryable<Movie> cinemaContext = _context.Movies;
            cinemaContext = Sort_Search(cinemaContext, sortOrder, movie.Genre ?? "");

            // Разбиение на страницы
            int count = cinemaContext.Count();
            cinemaContext = cinemaContext.Skip((page - 1) * pageSize).Take(pageSize);

            // Формирование модели для передачи представлению
            movie.SortViewModel = new SortViewModel(sortOrder);
            MoviesViewModel operations = new()
            {
                Movies = cinemaContext,
                PageViewModel = new PageViewModel(count, page, pageSize),
                MovieViewModel = movie
            };

            return View(operations);
        }

        // Сортировка и фильтрация данных
        private static IQueryable<Movie> Sort_Search(IQueryable<Movie> movies, SortState sortOrder, string searchGenre)
        {
            switch (sortOrder)
            {
                case SortState.GenreAsc:
                    movies = movies.OrderBy(s => s.Genre.Name);
                    break;
                case SortState.GenreDesc:
                    movies = movies.OrderByDescending(s => s.Genre.Name);
                    break;
            }
            movies = movies.Include(o => o.Genre)
                .Where(o => o.Genre.Name.Contains(searchGenre ?? ""));

            return movies;
        }
    }
}