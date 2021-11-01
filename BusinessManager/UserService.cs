using PersonalBlog.BusinessManager.Interfaces;
using PersonalBlog.Data;
using PersonalBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlog.BusinessManager
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext _context)
        {
            this._context = _context;
        }

        public async Task<ApplicationUser> Update(ApplicationUser applicationUser)
        {
            _context.Update(applicationUser);
            await _context.SaveChangesAsync();
            return applicationUser;
        }

        public ApplicationUser Get(string id)
        {
            return _context.Users.FirstOrDefault(user => user.Id == id);
        }
    }
}
