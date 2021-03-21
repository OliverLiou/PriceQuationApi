using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using PriceQuationApi.Model;
using Microsoft.AspNetCore.Identity;

namespace PriceQuationApi.Services
{
    public interface IUserService
    {
        Task<List<AdminRole>> GetRoles();
        Task<List<AdminUser>> FindUser(int pageSize, int currentPage, string sort, string order, string querySearch);
        Task<int> CountUser(string querySearch);
        Task<AdminUser> CreateUser(AdminUser user);
        Task<AdminUser> GetUser(string userId);
        Task<AdminUser> DeleteUser(AdminUser user);
        Task<AdminUser> UpdateUser(AdminUser user, List<string> roleNames);
    }

    public class UserService :IUserService
    {
        private readonly PriceQuationContext _context;
        private readonly UserManager<AdminUser> _userManager;
        private readonly RoleManager<AdminRole> _roleManager;

        public UserService (PriceQuationContext context, UserManager<AdminUser> userManager, RoleManager<AdminRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<List<AdminUser>> FindUser(int pageSize, int currentPage, string sort, string order, string querySearch)
        {
            try
            {
                var items = _userManager.Users.AsQueryable();
                // var items = _context.Users.AsQueryable();

                if (!String.IsNullOrEmpty(querySearch))
                {
                    items = PublicMethods.setWhereStr(querySearch, typeof(AdminUser).GetProperties(), items);
                }
                if (!String.IsNullOrEmpty(sort))
                {
                    items = items.OrderBy(sort + " " + order);
                }
                if (pageSize > 0 && currentPage > 0)
                {
                    items = items.Skip(pageSize * (currentPage - 1)).Take(pageSize);
                }
                var result = await items.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> CountUser(string querySearch)
        {
            try
            {
                var items = _userManager.Users.AsQueryable();
                // var items = _context.Users.AsQueryable();

                if (!String.IsNullOrEmpty(querySearch))
                {
                    items = PublicMethods.setWhereStr(querySearch, typeof(AdminUser).GetProperties(), items);
                }
                var result = await items.CountAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<AdminRole>> GetRoles()
        {
            try
            {
                var items = await _roleManager.Roles.ToListAsync();
                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AdminUser> CreateUser(AdminUser user)
        {
            try
            {
                var result = await _userManager.CreateAsync(user);
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AdminUser> GetUser(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AdminUser> DeleteUser(AdminUser user)
        {
            try
            {
                var item = await GetUser(user.Id);
                var result = await _userManager.DeleteAsync(item);
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AdminUser> UpdateUser(AdminUser user, List<string> roleNames)
        {
            try
            {
                var result = await _userManager.UpdateAsync(user);

                var roles = await _userManager.GetRolesAsync(user);
                var result1 = await _userManager.RemoveFromRolesAsync(user, roles);
                var result2 = await _userManager.AddToRolesAsync(user, roleNames);

                var item = await this.GetUser(user.Id);
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}