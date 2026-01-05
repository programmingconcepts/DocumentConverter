using System.ComponentModel.DataAnnotations;

namespace DocumentConverter.Models
{
    public class FileConversionModel
    {
        [Required(ErrorMessage = "Please select a file")]
        public IFormFile? File { get; set; }

        [Required(ErrorMessage = "Please select a conversion type")]
        public ConversionType ConversionType { get; set; }

        // Optional: display-only info
        public string? OriginalFileName { get; set; }
        public string? ConvertedFileName { get; set; }

        // Optional: result / error handling
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
