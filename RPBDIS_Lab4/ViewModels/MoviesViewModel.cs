using Azure;
using Microsoft.AspNetCore.Mvc.Rendering;
using RPBDIS_Lab4.Models;

namespace RPBDIS_Lab4.ViewModels
{
    public class MoviesViewModel
    {
        public IEnumerable<Movie> Movies { get; set; }

        //Свойство для фильтрации
        public MovieViewModel MovieViewModel { get; set; }
        //Свойство для навигации по страницам
        public PageViewModel PageViewModel { get; set; }
        //Список отчетных годов
        public SelectList ListYears { get; set; }
    }
}