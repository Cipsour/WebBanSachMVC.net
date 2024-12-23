namespace BookStoreMVCWeb.Models
{
	public class Order
	{
		public int Id { get; set; }
		public string OrderCode { get; set; }
		public string UserName { get; set; }
		public string PhoneNumber { get; set; }
		public bool IsApproved { get; set; }
		public bool IsCanceled { get; set; }
		public DateTime CreatedDate { get; set; }
		public int Status { get; set; }
	}
}
