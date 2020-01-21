using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGallery.Client.ViewModels
{
    public class OrderFrameViewModel
    {
        public string Address { get; private set; } = string.Empty;

        public OrderFrameViewModel(string address)
        {
            Address = address;
        }
    }
}
