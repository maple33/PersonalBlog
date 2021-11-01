using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using PersonalBlog.BusinessManager.Interfaces;
using PersonalBlog.Models;
using PersonalBlog.ViewModels.HomeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlog.BusinessManager
{
    public class HomeBusinessManager : IHomeBusinessManager
    {
        private readonly IBlogService blogService;
        private readonly IUserService userService;

        public HomeBusinessManager(IBlogService blogService, IUserService userService)
        {
            this.userService = userService;
            this.blogService = blogService;
        }

        public ActionResult<AuthorViewModel> GetAuthorViewModel(string authorId, string searchString, int? page)
        {
            if (authorId is null)
                return new BadRequestResult();

            var applicationUser = userService.Get(authorId);
            if (applicationUser is null)
                return new NotFoundResult();

            int pageSize = 20;
            int pageNumber = page ?? 1;

            var posts = blogService.GetBlogs(searchString ?? string.Empty)
                .Where(post => post.Published && post.Creator == applicationUser);

            return new AuthorViewModel
            {
                Author = applicationUser,
                Posts = new StaticPagedList<Post>(posts.Skip((pageNumber - 1) * pageSize).Take(pageSize), pageNumber, pageSize, posts.Count()),
                SearchString = searchString,
                PageNumber = pageNumber
            };
        }
    }
}
