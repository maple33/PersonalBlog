using PersonalBlog.BusinessManager.Interfaces;
using PersonalBlog.Data;
using PersonalBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlog.BusinessManager
{
    public class BlogService : IBlogService
    {
        private readonly ApplicationDbContext _context;
        public BlogService(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        public async Task<Blog> Add(Blog blog)
        {
            _context.Add(blog);
            await _context.SaveChangesAsync();
            return blog;
        }
    }
}
