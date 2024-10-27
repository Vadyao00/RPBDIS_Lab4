namespace RPBDIS_Lab4.ViewModels
{
    public enum SortState
    {
        No, // не сортировать
        ActorAsc,    // по актеру по возрастанию
        ActorDesc,   // по актеру по убыванию
        GenreAsc, // по жанру возрастанию
        GenreDesc    // по жанру по убыванию
    }
    public class SortViewModel
    {
        public SortState ActorSort { get; set; } // значение для сортировки по топливу
        public SortState GenreSort { get; set; }    // значение для сортировки по емкости
        public SortState CurrentState { get; set; }     // текущее значение сортировки

        public SortViewModel(SortState sortOrder)
        {
            ActorSort = sortOrder == SortState.ActorAsc ? SortState.ActorDesc : SortState.ActorAsc;
            GenreSort = sortOrder == SortState.GenreAsc ? SortState.GenreDesc : SortState.GenreAsc;
            CurrentState = sortOrder;
        }
    }
}