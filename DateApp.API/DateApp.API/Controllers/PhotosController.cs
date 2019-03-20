using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DateApp.API.Data;
using DateApp.API.Dto;
using DateApp.API.Models;
using Firebase.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DateApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;
        public PhotosController(IDatingRepository repo, IMapper mapper)
        {
            this._repo = repo;
            this._mapper = mapper;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> InsetPhoto(int id, [FromBody]PhotoForCreation photoCreation)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _repo.GetUser(id);

            var file = photoCreation.File;
            string nameFile = Guid.NewGuid().ToString();
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var task = await new FirebaseStorage("datingapp-4bbed.appspot.com")
                   .Child(nameFile)
                   .PutAsync(stream);

                    if (task != null)
                    {
                        photoCreation.Url = task.ToString();
                        photoCreation.FireBaseID = nameFile;
                        var photo = _mapper.Map<PhotoModel>(photoCreation);
                        if (!userFromRepo.Photos.Any(p => p.IsMain))
                            photo.IsMain = true;

                        userFromRepo.Photos.Add(photo);

                        if (await _repo.SaveAll())
                        {
                            var photoToReturn = _mapper.Map<PhotoForDetailDTO>(photo);
                            return CreatedAtRoute("GetPhoto", new { id = photo.ID, photoToReturn });
                        }
                    }
                }


            }

            return BadRequest();
        }
        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {

            var photoFromRepo = await _repo.GetPhoto(id);

            var photo = _mapper.Map<PhotoForDetailDTO>(photoFromRepo);

            return Ok(photo);
        }

        [HttpPost("{userId}/setMain/{id}")]
        public async Task<IActionResult> SetMain(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _repo.GetUser(userId);

            if (!userFromRepo.Photos.Any(d => d.ID == id))
                return Unauthorized();

            var photoFromRepo = await _repo.GetPhoto(id);

            if (photoFromRepo.IsMain)
                return BadRequest("This photo is already main");

            var currentMainPhoto = await _repo.GetMainPhotoForUser(userId);
            currentMainPhoto.IsMain = false;
            photoFromRepo.IsMain = true;

            if (await _repo.SaveAll())
                return NoContent();

            return BadRequest("could not set photo to main");
        }

        [HttpDelete("{userId}/deletePhoto/{id}")]
        public async Task<IActionResult> DeletePhoto(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _repo.GetUser(userId);

            if (!userFromRepo.Photos.Any(d => d.ID == id))
                return Unauthorized();

            var photoFromRepo = await _repo.GetPhoto(id);

            if (photoFromRepo.IsMain)
                return BadRequest("you cannot delete your main photo");

            await new FirebaseStorage("datingapp-4bbed.appspot.com").Child(photoFromRepo.FireBaseID).DeleteAsync();

            _repo.Delete(photoFromRepo);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("errror deleting the photo");
        }
    }
}