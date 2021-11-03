using Microsoft.EntityFrameworkCore;
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
        public async Task<Post> Add(Post post)
        {
            _context.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<Post> Update(Post post)
        {
            _context.Update(post);
            await _context.SaveChangesAsync();
            return post;
        }
        public IEnumerable<Post> GetBlogs(string searchString)
        {
            return _context.Blogs
                .OrderByDescending(post => post.UpdatedOn)
                .Include(post => post.Creator)
                .Include(post => post.Comments)
                .Where(post => post.Title.Contains(searchString) || post.Content.Contains(searchString));
        }

        public IEnumerable<Post> GetBlogs(ApplicationUser applicationUser)
        {
            return _context.Blogs
                .Include(post => post.Creator)
                .Include(post => post.Approver)
                .Include(post => post.Approver)
                .Where(post => post.Creator == applicationUser);
        }

        public Post GetBlog(int postId)
        {
            return _context.Blogs
                .Include(post => post.Creator)
                .Include(post => post.Comments)
                    .ThenInclude(comment => comment.Poster)
                .Include(post => post.Comments)
                    .ThenInclude(comment => comment.Comments)
                        .ThenInclude(reply => reply.Parent)
                .FirstOrDefault(post => post.Id == postId);
        }

        public Comment GetComment(int commentId)
        {
            return _context.Posts
                .Include(comment => comment.Poster)
                .Include(comment => comment.Post)
                .Include(comment => comment.Parent)
                .FirstOrDefault(comment => comment.Id == commentId);
        }
        public async Task<Comment> Add(Comment comment)
        {
            _context.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
    }
}
