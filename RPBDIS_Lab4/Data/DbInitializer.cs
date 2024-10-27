using RPBDIS_Lab4.Models;

namespace RPBDIS_Lab4.Data
{
    public static class DbInitializer
    {
        public static  void Initialize(CinemaContext db)
        {
            db.Database.EnsureCreated();

            if (db.Actors.Any())
            {
                return;
            }

            Random randObj = new(1);

            // Заполнение таблицы жанров
            string[] genreNames = { "Комедия", "Драма", "Боевик", "Ужасы", "Фантастика", "Приключения", "Анимация", "Триллер", "Детектив", "Фэнтези" };
            string[] genreDescriptions = { "Смешной", "Серьезный", "Экшн", "Страшный", "Научный", "Захватывающий", "Мультфильм", "Напряженный", "Загадочный", "Воображаемый" };
            int genreCount = genreNames.Length;

            for (int i = 1; i <= 40; i++)
            {
                db.Genres.Add(new Genre
                {
                    GenreId = Guid.NewGuid(),
                    Name = genreNames[i % genreCount] + $"_{i}",
                    Description = genreDescriptions[i % genreCount]
                });
            }
            db.SaveChanges();

            // Заполнение таблицы актеров
            string[] actorNames = { "Актер_", "Актриса_", "Режиссер_", "Продюсер_", "Сценарист_" };
            int actorNameCount = actorNames.Length;

            for (int i = 1; i <= 40; i++)
            {
                db.Actors.Add(new Actor
                {
                    ActorId = Guid.NewGuid(),
                    Name = actorNames[i % actorNameCount] + i.ToString()
                });
            }
            db.SaveChanges();

            // Заполнение таблицы фильмов
            string[] movieTitles = { "Фильм_", "История_", "Сага_", "Приключение_", "Эксперимент_" };
            string[] productionCompanies = { "Warner Bros", "Paramount Pictures", "Universal Pictures", "20th Century Fox", "Columbia Pictures" };
            string[] countries = { "США", "Великобритания", "Канада", "Австралия", "Франция" };
            int titleCount = movieTitles.Length;
            int companyCount = productionCompanies.Length;
            int countryCount = countries.Length;

            for (int i = 1; i <= 300; i++)
            {
                var genre = db.Genres.OrderBy(g => Guid.NewGuid()).First();
                var actors = db.Actors.OrderBy(a => Guid.NewGuid()).Take(2).ToList();

                db.Movies.Add(new Movie
                {
                    MovieId = Guid.NewGuid(),
                    Title = movieTitles[i % titleCount] + i.ToString(),
                    Duration = TimeOnly.FromTimeSpan(TimeSpan.FromMinutes(90 + randObj.Next(60))),
                    ProductionCompany = productionCompanies[i % companyCount],
                    Country = countries[i % countryCount],
                    AgeRestriction = 13 + randObj.Next(5) * 3,
                    Description = "Описание фильма " + movieTitles[i % titleCount] + i.ToString(),
                    GenreId = genre.GenreId,
                    Genre = genre,
                    Actors = actors
                });
            }
            db.SaveChanges();
        }
    }
}