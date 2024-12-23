namespace BookStoreMVCWeb.Models.ViewModel
{
    public class PagedBooksViewModel
    {
        public List<Book> Books { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string SelectedCategory { get; set; }
        public int PageSize { get; set; }
    }
}
