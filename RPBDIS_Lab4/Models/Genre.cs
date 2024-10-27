using System.ComponentModel.DataAnnotations;

namespace RPBDIS_Lab4.Models
{
    public partial class Genre
    {
        public Guid GenreId { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; } = null!;

        [Display(Name = "Описание")]
        public string? Description { get; set; }

        public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
        public Genre()
        {
            Movies = new List<Movie>();
        }
    }
}