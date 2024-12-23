using Microsoft.AspNetCore.Identity;

namespace BookStoreMVCWeb.Models
{
	public class AppUser:IdentityUser
	{
		public string Opccupation { get; set; }
		public string PhoneNumber { get; set; }
	}
}
