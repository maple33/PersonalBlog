using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.BusinessManager.Interfaces;
using PersonalBlog.ViewModels;

namespace PersonalBlog.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogBusinessManager blogBusinessManager;
        public BlogController(IBlogBusinessManager blogBusinessManager)
        {
            this.blogBusinessManager = blogBusinessManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View(new CreateViewModel());
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var actionResult = await blogBusinessManager.GetEditViewModel(id, User);

            if (actionResult is null)
                return View(actionResult.Value);

            return actionResult.Result;
        }
        [HttpPost]
        public async Task<IActionResult> Add(CreateViewModel createBlogViewModel)
        {
            await blogBusinessManager.CreateBlogAsync(createBlogViewModel, User);
            return RedirectToAction("Create", createBlogViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Update(EditViewModel editViewModel)
        {
            var actionResult = await blogBusinessManager.UpdateBlog(editViewModel, User);

            if (actionResult.Result is null)
                return RedirectToAction("Edit", new { editViewModel.Post.Id });

            return actionResult.Result;
        }
    }
}
