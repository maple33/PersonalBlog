using Microsoft.AspNetCore.Identity;
using PersonalBlog.BusinessManager.Interfaces;
using PersonalBlog.Models;
using PersonalBlog.ViewModels.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PersonalBlog.BusinessManager
{
    public class AdminBusinessManager : IAdminBusinessManager
    {
        private UserManager<ApplicationUser> userManager;
        private IBlogService blogService;
        public AdminBusinessManager(UserManager<ApplicationUser> userManager, IBlogService blogService)
        {
            this.userManager = userManager;
            this.blogService = blogService;
        }
        public async Task<IndexViewModel> GetAdminDashboard(ClaimsPrincipal claimsPrincipal)
        {
            var applicationUser = await userManager.GetUserAsync(claimsPrincipal);
            return new IndexViewModel
            {
                Posts = blogService.GetBlogs(applicationUser)
            };
        }
    }
}