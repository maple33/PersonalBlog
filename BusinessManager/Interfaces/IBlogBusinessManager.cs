using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Models;
using PersonalBlog.ViewModels;
using PersonalBlog.ViewModels.HomeViewModels;
using PersonalBlog.ViewModels.PostViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PersonalBlog.BusinessManager.Interfaces
{
    public interface IBlogBusinessManager
    {
        IndexViewModel GetIndexViewModel(string searchString, int? page);
        Task<Post> CreateBlogAsync(CreateViewModel createBlogViewModel, ClaimsPrincipal claimsPrincipal);
        Task<ActionResult<EditViewModel>> GetEditViewModel(int? id, ClaimsPrincipal claimsPrincipal);
        Task<ActionResult<EditViewModel>> UpdateBlog(EditViewModel editViewModel, ClaimsPrincipal claimsPrincipal);
        Task<ActionResult<PostViewModel>> GetPostViewModel(int? id, ClaimsPrincipal claimsPrincipal);
    }
}
