namespace BookStoreMVCWeb.Models.ViewModel
{
    public class ReviewVm
    {
        public int BookId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
		public List<Review> Reviews { get; set; }
	}
}
