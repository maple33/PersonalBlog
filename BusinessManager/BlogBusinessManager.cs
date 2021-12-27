using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using PersonalBlog.Authorization;
using PersonalBlog.BusinessManager.Interfaces;
using PersonalBlog.Data;
using PersonalBlog.Models;
using PersonalBlog.ViewModels;
using PersonalBlog.ViewModels.HomeViewModels;
using PersonalBlog.ViewModels.PostViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PersonalBlog.BusinessManager
{
    public class BlogBusinessManager : IBlogBusinessManager
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IBlogService blogService;
        private readonly IWebHostEnvironment webHostEnviroment;
        private readonly IAuthorizationService authorizationService;


        public BlogBusinessManager(UserManager<ApplicationUser> userManager, IBlogService blogService, 
            IWebHostEnvironment webHostEnvironment, IAuthorizationService authorizationService)
        {
            this.userManager = userManager;
            this.blogService = blogService;
            this.webHostEnviroment = webHostEnvironment;
            this.authorizationService = authorizationService;
        }



        public async Task<Post> CreateBlogAsync(CreateViewModel createViewModel, ClaimsPrincipal claimsPrincipal)
        {
            Post post = createViewModel.Post;
            post.Creator = await userManager.GetUserAsync(claimsPrincipal);
            post.CreatedOn = DateTime.Now;
            post.UpdatedOn = DateTime.Now;

            post = await blogService.Add(post);

            if (createViewModel.BlogHeaderImage != null)
            {
                string webRootPath = webHostEnviroment.WebRootPath;
                string pathToImage = $@"{webRootPath}\UserFiles\Blogs\{post.Id}\HeaderImage.jpg";
                EnsureFolder(pathToImage);

                using (var fileStream = new FileStream(pathToImage, FileMode.Create))
                {
                    await createViewModel.BlogHeaderImage.CopyToAsync(fileStream);
                }
            }

            return post;
        }

        public async Task<ActionResult<Comment>> CreateComment(PostViewModel postViewModel, ClaimsPrincipal claimsPrincipal)
        {
            if (postViewModel.Post is null || postViewModel.Post.Id == 0)
                return new BadRequestResult();

            var post = blogService.GetBlog(postViewModel.Post.Id);
            if (post is null)
                return new NotFoundResult();

            var comment = postViewModel.Comment;

            comment.Poster = await userManager.GetUserAsync(claimsPrincipal);
            comment.Post = post;
            comment.CreatedOn = DateTime.Now;

            if (comment.Parent != null)
            {
                comment.Parent = blogService.GetComment(comment.Parent.Id);
            }

            return await blogService.Add(comment);
        }


        public async Task<ActionResult<EditViewModel>> GetEditViewModel(int? id, ClaimsPrincipal claimsPrincipal)
        {
            if (id is null)
                return new BadRequestResult();

            var postId = id.Value;
            var post = blogService.GetBlog(postId);
            if (post is null)
                return new NotFoundResult();

            var authorizationResult = await authorizationService.AuthorizeAsync(claimsPrincipal, post, Operations.Update);
            if (!authorizationResult.Succeeded)
                return DetermineActionResult(claimsPrincipal);
            
            return new EditViewModel
            {
                Post = post
            };

        }


        public IndexViewModel GetIndexViewModel(string searchString, int? page)
        {
            int pageSize = 20;
            int pageNumber = page ?? 1;
            var posts = blogService.GetBlogs(searchString ?? string.Empty)
                .Where(post => post.Published);

            return new IndexViewModel
            {
                Posts = new StaticPagedList<Post>(posts.Skip((pageNumber - 1) * pageSize).Take(pageSize), pageNumber, pageSize, posts.Count()),
                SearchString = searchString,
                PageNumber = pageNumber
            };
        }

        public async Task<ActionResult<PostViewModel>> GetPostViewModel(int? id, ClaimsPrincipal claimsPrincipal)
        {
            if (id is null)
                return new BadRequestResult();

            var postId = id.Value;
            var post = blogService.GetBlog(postId);
            if (post is null)
                return new NotFoundResult();

            if (!post.Published)
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(claimsPrincipal, post, Operations.Read);
                if (!authorizationResult.Succeeded)
                    return DetermineActionResult(claimsPrincipal);
            }
            return new PostViewModel
            {
                Post = post
            };
        }

        public async Task<ActionResult<EditViewModel>> UpdateBlog(EditViewModel editViewModel, ClaimsPrincipal claimsPrincipal)
        {
            var post = blogService.GetBlog(editViewModel.Post.Id);
            if (post is null)
                return new NotFoundResult();

            var authorizationResult = await authorizationService.AuthorizeAsync(claimsPrincipal, post, Operations.Update);
            if (!authorizationResult.Succeeded)
                return DetermineActionResult(claimsPrincipal);

            post.Published = editViewModel.Post.Published;
            post.Title = editViewModel.Post.Title;
            post.Content = editViewModel.Post.Content;
            post.UpdatedOn = DateTime.Now;

            if (editViewModel.BlogHeaderImage != null)
            {
                string webRootPath = webHostEnviroment.WebRootPath;
                string pathToImage = $@"{webRootPath}\UserFiles\Blogs\{post.Id}\HeaderImage.jpg";
                EnsureFolder(pathToImage);

                using (var fileStream = new FileStream(pathToImage, FileMode.Create))
                {
                    await editViewModel.BlogHeaderImage.CopyToAsync(fileStream);
                }
            }

            return new EditViewModel
            {
                Post = await blogService.Update(post)
            };
        }


        private ActionResult DetermineActionResult(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
                return new ForbidResult();
            else
                return new ChallengeResult();
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
