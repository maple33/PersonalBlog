using PersonalBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlog.BusinessManager.Interfaces
{
    public interface IBlogService
    {
        Task<Post> Add(Post post);
        IEnumerable<Post> GetBlogs(ApplicationUser applicationUser);
        Post GetBlog(int postId);
        Task<Post> Update(Post post);
        IEnumerable<Post> GetBlogs(string searchString);
    }
}
