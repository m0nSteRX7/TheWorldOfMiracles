using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITCareerProject.Data;
using ITCareerProject.Services.DTOs.Users;
using ITCareerProject.Services.RolesService;

namespace ITCareerProject.Services.UsersService
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IRolesService _rolesService;

        public UsersService(ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IRolesService rolesService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _mapper = mapper;
            _rolesService = rolesService;
        }

        public async Task<ICollection<BaseUserDto>> GetAllAsync()
        {
            var users = await _dbContext
                .ApplicationUsers
                .AsNoTracking()
                .ToListAsync();

            var usersDtos = _mapper.Map<List<BaseUserDto>>(users);
            usersDtos.ForEach(u =>
            {
                u.RoleName = _rolesService.GetRoleNameByUserId(u.Id).GetAwaiter().GetResult();
            });
            return usersDtos;

        }

        public async Task<ICollection<BaseUserDto>> GetFilteredUsers(string keyword)
        {
            var users = await GetAllAsync();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                users = users
                    .Where(u => u.Username.Contains(keyword, StringComparison.InvariantCultureIgnoreCase)
                                || u.FirstName.Contains(keyword, StringComparison.InvariantCultureIgnoreCase)
                                || u.LastName.Contains(keyword, StringComparison.InvariantCultureIgnoreCase)
                                || u.RoleName.Contains(keyword, StringComparison.InvariantCultureIgnoreCase))
                    .ToArray();
            }

            return users.ToList();
        }

        public async Task<BaseUserDto?> GetByIdAsync(string userId)
        {
            var user = await _dbContext
                .ApplicationUsers
                .FirstOrDefaultAsync(u => u.Id == userId);

            var mappedUser = _mapper.Map<BaseUserDto>(user);
            mappedUser.RoleName = await _rolesService.GetRoleNameByUserId(mappedUser.Id);
            return mappedUser;
        }

        public async Task CreateAsync(CreateUserDto userDto)
        {
            var userToBeCreated = new ApplicationUser()
            {
                UserName = userDto.Username,
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
            };

            var createdUser = await _userManager.CreateAsync(userToBeCreated, userDto.Password);

            if (createdUser.Succeeded)
            {
                await _userManager.AddToRoleAsync(userToBeCreated, DefaultRoles.User.ToString());
            }
            else
            {
                throw new InvalidOperationException(createdUser.Errors.First().Description);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(BaseUserDto userDto)
        {
            var domainUser = await _dbContext
                .ApplicationUsers
                .FirstOrDefaultAsync(u => u.Id == userDto.Id);

            if (domainUser == null) return;

            domainUser.FirstName = userDto.FirstName;
            domainUser.LastName = userDto.LastName;
            domainUser.Email = userDto.Email;
            domainUser.NormalizedEmail = userDto.Email.Normalize();
            domainUser.UserName = userDto.Username;
            domainUser.NormalizedUserName = userDto.Username.Normalize();

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string userId)
        {
            var user = await _dbContext
                .ApplicationUsers
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return;

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UserExists(string userId)
        {
            return await _dbContext
                .ApplicationUsers
                .AnyAsync(u => u.Id == userId);
        }

        public int GetUsersCount()
        {
            return _dbContext.ApplicationUsers.Count();
        }
    }
}
