﻿namespace BookStoreMVCWeb.Models
{
	public class OrderDeatails
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string PhoneNumber { get; set; }
		public string OrderCode { get; set;}
		public int BookId { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
		
	}
}