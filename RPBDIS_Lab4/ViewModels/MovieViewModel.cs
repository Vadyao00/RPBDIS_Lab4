using RPBDIS_Lab4.Models;
using System.ComponentModel.DataAnnotations;

namespace RPBDIS_Lab4.ViewModels
{
    public class MovieViewModel
    {
        public Guid MovieId { get; set; }

        [Display(Name = "Название")]
        public string Title { get; set; } = null!;

        [Display(Name = "Продолжительность")]
        public TimeOnly Duration { get; set; }

        [Display(Name = "Компания")]
        public string? ProductionCompany { get; set; }

        [Display(Name = "Страна")]
        public string? Country { get; set; }

        [Display(Name = "Возрастное ограничение")]
        public int? AgeRestriction { get; set; }

        [Display(Name = "Описание")]
        public string? Description { get; set; }

        [Display(Name = "Жанр")]
        public string? Genre { get; set; }

        public SortViewModel SortViewModel { get; set; }
    }
}