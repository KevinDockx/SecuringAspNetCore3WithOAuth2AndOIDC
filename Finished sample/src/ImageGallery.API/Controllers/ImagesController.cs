using AutoMapper;
using ImageGallery.API.Services;
using ImageGallery.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ImageGallery.API.Controllers
{
    [Route("api/images")]
    [ApiController]
    [Authorize]
    public class ImagesController : ControllerBase
    {
        private readonly IGalleryRepository _galleryRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;

        public ImagesController(
            IGalleryRepository galleryRepository,
            IWebHostEnvironment hostingEnvironment,
            IMapper mapper)
        {
            _galleryRepository = galleryRepository ?? 
                throw new ArgumentNullException(nameof(galleryRepository));
            _hostingEnvironment = hostingEnvironment ?? 
                throw new ArgumentNullException(nameof(hostingEnvironment));
            _mapper = mapper ?? 
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet()]
        public IActionResult GetImages()
        {
            var ownerId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            
            // get from repo
            var imagesFromRepo = _galleryRepository.GetImages(ownerId);

            // map to model
            var imagesToReturn = _mapper.Map<IEnumerable<Model.Image>>(imagesFromRepo);

            // return
            return Ok(imagesToReturn);
        }

        [HttpGet("{id}", Name = "GetImage")]
        [Authorize("MustOwnImage")]
        public IActionResult GetImage(Guid id)
        {          
            var imageFromRepo = _galleryRepository.GetImage(id);

            if (imageFromRepo == null)
            {
                return NotFound();
            }

            var imageToReturn = _mapper.Map<Model.Image>(imageFromRepo);

            return Ok(imageToReturn);
        }

        [HttpPost()]
        [Authorize(Roles = "PayingUser")]
        public IActionResult CreateImage([FromBody] ImageForCreation imageForCreation)
        {
            // Automapper maps only the Title in our configuration
            var imageEntity = _mapper.Map<Entities.Image>(imageForCreation);

            // Create an image from the passed-in bytes (Base64), and 
            // set the filename on the image

            // get this environment's web root path (the path
            // from which static content, like an image, is served)
            var webRootPath = _hostingEnvironment.WebRootPath;

            // create the filename
            string fileName = Guid.NewGuid().ToString() + ".jpg";
            
            // the full file path
            var filePath = Path.Combine($"{webRootPath}/images/{fileName}");

            // write bytes and auto-close stream
            System.IO.File.WriteAllBytes(filePath, imageForCreation.Bytes);

            // fill out the filename
            imageEntity.FileName = fileName;

            // set the ownerId on the imageEntity
            var ownerId = User.Claims.FirstOrDefault(c => c.Type == "sub").Value;
            imageEntity.OwnerId = ownerId;

            // add and save.  
            _galleryRepository.AddImage(imageEntity);

            _galleryRepository.Save();

            var imageToReturn = _mapper.Map<Image>(imageEntity);

            return CreatedAtRoute("GetImage",
                new { id = imageToReturn.Id },
                imageToReturn);
        }

        [HttpDelete("{id}")]
        [Authorize("MustOwnImage")]
        public IActionResult DeleteImage(Guid id)
        {            
            var imageFromRepo = _galleryRepository.GetImage(id);

            if (imageFromRepo == null)
            {
                return NotFound();
            }

            _galleryRepository.DeleteImage(imageFromRepo);

            _galleryRepository.Save();

            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize("MustOwnImage")]
        public IActionResult UpdateImage(Guid id, 
            [FromBody] ImageForUpdate imageForUpdate)
        {
            var imageFromRepo = _galleryRepository.GetImage(id);
            if (imageFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(imageForUpdate, imageFromRepo);

            _galleryRepository.UpdateImage(imageFromRepo);

            _galleryRepository.Save();

            return NoContent();
        }
    }
}