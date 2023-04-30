using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITCareerProject.Data;
using ITCareerProject.Services.DTOs.Users;

namespace ITCareerProject.Services.UsersService
{
    public interface IUsersService
    {
        Task<ICollection<BaseUserDto>> GetAllAsync();
        Task<ICollection<BaseUserDto>> GetFilteredUsers(string keyword);
        Task<BaseUserDto?> GetByIdAsync(string userId);
        Task CreateAsync(CreateUserDto userDto);
        Task EditAsync(BaseUserDto userDto);
        Task DeleteAsync(string userId);
        Task<bool> UserExists(string userId);
        int GetUsersCount();
    }
}
