using System.ComponentModel.DataAnnotations;

namespace ImageGallery.Model
{
    public class ImageForUpdate
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }      
    }
}
