using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController(IUserRepository userRepository) : BaseApiController
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
    }
}
