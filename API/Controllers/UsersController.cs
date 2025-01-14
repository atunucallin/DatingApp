using System.Security.Claims;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService) : BaseApiController
    {


        [HttpGet]

        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()

        {

            var users = await userRepository.GetMemebersAsync();

            return Ok(users);
        }

        [HttpGet("{username}")]

        public async Task<ActionResult<MemberDto>> GetUser(string username)

        {

            var user = await userRepository.GetMemberByUserNameAsync(username);

            return Ok(user);
        }

        [HttpPut]

        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)

        {



            var user = await userRepository.GetUserByUserNameAsync(User.GetUsername());

            if (user == null) return BadRequest("Could not find user");


            mapper.Map(memberUpdateDto, user);

            if (await userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update the User");
        }


        [HttpPost("add-photo")]

        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)

        {
            var user = await userRepository.GetUserByUserNameAsync(User.GetUsername());
            if (user == null) return BadRequest("Could not find user");

            var result = await photoService.AddPhotoAsync(file);
            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {

                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
            };
            user.Photos.Add(photo);
            if (await userRepository.SaveAllAsync()) return CreatedAtAction(nameof(GetUser),
            new { username = user.UserName }, mapper.Map<PhotoDto>(photo));

            return BadRequest("Problem Adding photo");
        }

        [HttpPut("set-main-photo/{photoId:int}")]

        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            var user = await userRepository.GetUserByUserNameAsync(User.GetUsername());
            if (user == null) return BadRequest("Could not find user");

            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo == null || photo.Ismain) return BadRequest("Cannot use this as a main photo");

            var currentMain = user.Photos.FirstOrDefault(x => x.Ismain);
            if (currentMain != null) currentMain.Ismain = false;
            photo.Ismain = true;
            if (await userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Problem Setting main Photo");
        }

        [HttpDelete("delete-photo/{photoId:int}")]

        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var user = await userRepository.GetUserByUserNameAsync(User.GetUsername());
            if (user == null) return BadRequest("Could not find user");

            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);
            if (photo == null || photo.Ismain) return BadRequest("This Photo cannot be deleted");

            if (photo.PublicId !=null){

                var result = await photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null) return BadRequest(result.Error.Message);
            }
            user.Photos.Remove(photo);
            if (await userRepository.SaveAllAsync()) return Ok();
            
            return BadRequest("Problem Deleting Photo");
        }
    }
}
