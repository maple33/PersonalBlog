using PersonalBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlog.ViewModels.PostViewModels
{
    public class PostViewModel
    {
        public Post Post { get; set; }
        public Comment Comment { get; set; }
    }
}
