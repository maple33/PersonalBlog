using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlog.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public Post Post { get; set; }
        public ApplicationUser Poster { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Content { get; set; }
        public Comment Parent { get; set; }
        public virtual IEnumerable<Comment> Comments { get; set; }
    }
}
