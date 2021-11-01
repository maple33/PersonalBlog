using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using PersonalBlog.BusinessManager.Interfaces;
using PersonalBlog.Models;
using PersonalBlog.ViewModels.AboutViewModels;
using PersonalBlog.ViewModels.AdminViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PersonalBlog.BusinessManager
{
    public class AdminBusinessManager : IAdminBusinessManager
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IBlogService blogService;
        private readonly IUserService userService;
        private readonly IWebHostEnvironment webHostEnvironment;
        public AdminBusinessManager(UserManager<ApplicationUser> userManager, IBlogService blogService, IUserService userService, IWebHostEnvironment webHostEnvironment)
        {
            this.userManager = userManager;
            this.blogService = blogService;
            this.userService = userService;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<IndexViewModel> GetAdminDashboard(ClaimsPrincipal claimsPrincipal)
        {
            var applicationUser = await userManager.GetUserAsync(claimsPrincipal);
            return new IndexViewModel
            {
                Posts = blogService.GetBlogs(applicationUser)
            };
        }

        public async Task<AboutViewModel> GetAboutViewModel(ClaimsPrincipal claimsPrincipal)
        {
            var applicationUser = await userManager.GetUserAsync(claimsPrincipal);
            return new AboutViewModel
            {
                ApplicationUser = applicationUser,
                SubHeader = applicationUser.SubHeader,
                Content = applicationUser.Content
            };
        }

        public async Task UpdateAbout(ClaimsPrincipal claimsPrincipal, AboutViewModel aboutViewModel)
        {
            var applicationUser = await userManager.GetUserAsync(claimsPrincipal);

            applicationUser.SubHeader = aboutViewModel.SubHeader;
            applicationUser.Content = aboutViewModel.Content;
            if (aboutViewModel.HeaderImage != null)
            {
                string webRootPath = webHostEnvironment.WebRootPath;
                string pathToImage = $@"{webRootPath}\UserFiles\Users\{applicationUser.Id}\HeaderImage.jpg";
                EnsureFolder(pathToImage);

                using (var fileStream = new FileStream(pathToImage, FileMode.Create))
                {
                    await aboutViewModel.HeaderImage.CopyToAsync(fileStream);
                }
            }

            await userService.Update(applicationUser);
        }

        private void EnsureFolder(string path)
        {
            string directoryName = Path.GetDirectoryName(path);
            if (directoryName.Length > 0)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
        }
    }
}