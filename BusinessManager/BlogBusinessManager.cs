﻿using Microsoft.AspNetCore.Identity;
using PersonalBlog.BusinessManager.Interfaces;
using PersonalBlog.Data;
using PersonalBlog.Models;
using PersonalBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PersonalBlog.BusinessManager
{
    public class BlogBusinessManager : IBlogBusinessManager
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IBlogService blogService;
        public BlogBusinessManager(UserManager<ApplicationUser> userManager, IBlogService blogService)
        {
            this.userManager = userManager;
            this.blogService = blogService;
        }
        public async Task<Blog> CreateBlogAsync(CreateBlogViewModel createBlogViewModel, ClaimsPrincipal claimsPrincipal)
        {
            Blog blog = createBlogViewModel.Blog;
            blog.Creator = await userManager.GetUserAsync(claimsPrincipal);
            blog.CreatedOn = DateTime.Now;

            return await blogService.Add(blog);

        }
    }
}
