using BlogApiRubicon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApiRubicon.Responses
{
    public class SingleBlogPostResponse
    {
        public BlogResponse BlogPost { get; set; }
        public SingleBlogPostResponse(BlogResponse blogResponse)
        {
            BlogPost = blogResponse;
        }

        public SingleBlogPostResponse(Blog blog)
        {
            BlogPost = new BlogResponse(blog);
        }
    }
}
