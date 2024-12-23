using System.ComponentModel.DataAnnotations;

namespace BookStoreMVCWeb.Models.ViewModel
{
    public class RoleVm
    {
        [Required]
      public string RoleName { get; set; }
    }
}
