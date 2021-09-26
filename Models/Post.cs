using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlog.Models
{
    public class Post
    {
        public int Id { get; set; }
        public Blog Blog { get; set; }
        public ApplicationUser Poster { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Content { get; set; }
        public Post Parent { get; set; }
    }
}
