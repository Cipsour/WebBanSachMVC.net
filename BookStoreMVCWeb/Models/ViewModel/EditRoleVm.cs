using System.ComponentModel.DataAnnotations;

namespace BookStoreMVCWeb.Models.ViewModel
{
	public class EditRoleVm
	{
		public string Id { get; set; }
		[Display(Name ="Role")]
		[Required]
		public string RoleName { get; set; }
		public List<string> Users { get; set; } = new();
	}
}
