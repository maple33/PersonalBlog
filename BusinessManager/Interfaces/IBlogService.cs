using PersonalBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlog.BusinessManager.Interfaces
{
    public interface IBlogService
    {
        Task<Blog> Add(Blog blog);
    }
}
