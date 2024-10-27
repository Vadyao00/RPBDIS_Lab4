using System.ComponentModel.DataAnnotations;

namespace RPBDIS_Lab4.Models
{
    public partial class Actor
    {
        public Guid ActorId { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; } = null!;

        public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}