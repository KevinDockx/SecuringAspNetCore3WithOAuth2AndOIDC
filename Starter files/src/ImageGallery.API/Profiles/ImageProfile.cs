using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGallery.API.Profiles
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            // map from Image (entity) to Image, and back
            CreateMap<Entities.Image, Model.Image>().ReverseMap();

            // map from ImageForCreation to Image
            // Ignore properties that shouldn't be mapped
            CreateMap<Model.ImageForCreation, Entities.Image>()
                .ForMember(m => m.FileName, options => options.Ignore())
                .ForMember(m => m.Id, options => options.Ignore())
                .ForMember(m => m.OwnerId, options => options.Ignore());

            // map from ImageForUpdate to Image
            // ignore properties that shouldn't be mapped
            CreateMap<Model.ImageForUpdate, Entities.Image>()
                .ForMember(m => m.FileName, options => options.Ignore())
                .ForMember(m => m.Id, options => options.Ignore())
                .ForMember(m => m.OwnerId, options => options.Ignore());
        }
    }
}
