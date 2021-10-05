using PagedList.Core;
using PersonalBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlog.ViewModels.HomeViewModels
{
    public class IndexViewModel
    {
        public IPagedList<Post> Posts { get; set; }
        public string SearchString { get; set; }
        public int PageNumber { get; set; }
    }
}
