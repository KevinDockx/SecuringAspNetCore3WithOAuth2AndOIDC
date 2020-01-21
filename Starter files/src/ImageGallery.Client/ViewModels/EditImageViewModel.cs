using System;
using System.ComponentModel.DataAnnotations;

namespace ImageGallery.Client.ViewModels
{
    public class EditImageViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public Guid Id { get; set; }  
    }
}
