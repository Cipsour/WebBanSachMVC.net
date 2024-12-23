using System.ComponentModel.DataAnnotations;

namespace BookStoreMVCWeb.Models
{
	public class UserVm
	{
		public int Id { get; set; }
		[Required(ErrorMessage ="Vui lòng nhập UserName")]
        public string UserName { get; set; }
		[Required(ErrorMessage ="Làm ơn nhập Email"), EmailAddress]
		public string Email { get; set; }
		[DataType(DataType.Password), Required(ErrorMessage ="Làm ơn nhập Password")]
		public string Password { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Nghề nghiệp")]
		public string Opccupation { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Số Điện Thoại")]
        public string PhoneNumber { get; set; }

    }
}
