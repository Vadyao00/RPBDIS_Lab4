using Microsoft.Extensions.Caching.Memory;
using RPBDIS_Lab4.Services;
using RPBDIS_Lab4.ViewModels;

namespace RPBDIS_Lab4.Middleware
{
    public class DbCacheMiddleware(RequestDelegate next, IMemoryCache memoryCache, string cacheKey = "Movies 10")
    {
        private readonly RequestDelegate _next = next;
        private readonly IMemoryCache _memoryCache = memoryCache;
        private readonly string _cacheKey = cacheKey;

        public Task Invoke(HttpContext httpContext, IMovieService movieService)
        {
            // пытаемся получить элемент из кэша
            if (!_memoryCache.TryGetValue(_cacheKey, out HomeViewModel homeViewModel))
            {
                // если в кэше не найден элемент, получаем его от сервиса
                homeViewModel = movieService.GetHomeViewModel();
                // и сохраняем в кэше
                _memoryCache.Set(_cacheKey, homeViewModel,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(266)));
            }

            return _next(httpContext);
        }
    }

    // Метод расширения, используемый для добавления промежуточного программного обеспечения в конвейер HTTP-запроса.
    public static class DbCacheMiddlewareExtensions
    {
        public static IApplicationBuilder UseMovieCache(this IApplicationBuilder builder, string cacheKey)
        {
            return builder.UseMiddleware<DbCacheMiddleware>(cacheKey);
        }
    }
}