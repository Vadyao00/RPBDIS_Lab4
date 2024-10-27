using RPBDIS_Lab4.ViewModels;

namespace RPBDIS_Lab4.Services
{
    public interface IMovieService
    {
        HomeViewModel GetHomeViewModel(int numberRows = 10);
    }
}