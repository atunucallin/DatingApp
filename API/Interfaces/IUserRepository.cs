using System;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces;

public interface IUserRepository
{

    void Update (AppUser user);

    Task<bool> SaveAllAsync();

    Task<IEnumerable<AppUser>> GetUsersAsync();

    Task<AppUser?> GetUserByIdAsync(int id);

    Task<AppUser?> GetUserByUserNameAsync(string username);

    // Task<IEnumerable<MemberDto?>> GetMemebersAsync();

    Task<PagedList<MemberDto?>> GetMemebersAsync( UserParams userParams);

    Task<MemberDto?> GetMemberByUserNameAsync(string username);


}
