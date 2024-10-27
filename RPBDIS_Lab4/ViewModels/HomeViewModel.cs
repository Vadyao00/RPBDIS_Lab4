using RPBDIS_Lab4.Models;

namespace RPBDIS_Lab4.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<Actor> Actors { get; set; }
        public IEnumerable<MovieViewModel> Movies { get; set; }
    }
}