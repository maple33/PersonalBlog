using PersonalBlog.Models;
using PersonalBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PersonalBlog.BusinessManager.Interfaces
{
    public interface IBlogBusinessManager
    {
        Task<Blog> CreateBlogAsync(CreateBlogViewModel createBlogViewModel, ClaimsPrincipal claimsPrincipal);
    }
}
