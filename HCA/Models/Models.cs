using System;
using System.ComponentModel.DataAnnotations;

namespace HCA.Models
{
    public class Book
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Price {  get; set; }
    }
    public class ApiSettings
    {
        public string BaseUrl { get; set; }
    }

}
