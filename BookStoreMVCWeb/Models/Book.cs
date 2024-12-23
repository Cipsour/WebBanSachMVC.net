using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BookStoreMVCWeb.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }
        public string Language { get; set; }

        [Required,
        MaxLength(13)]
        public string IBNS { get; set; }
        [Required,
        MaxLength(13)]
        public string Category { get; set; }

        [Required,
        DataType(DataType.Date),
        Display(Name = "Date Published")]
        public DateTime DatePublished { get; set; }

        [Required,
        DataType(DataType.Currency)]
        public int Price { get; set; }

        [Required]
        public string Author { get; set; }

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }
    }
}
