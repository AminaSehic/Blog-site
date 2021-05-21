using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApiRubicon.Responses
{
    public class MultipleBlogPostsResponse
    {
        public List<BlogResponse> BlogPosts { get; set; }
        public int PostsCount { get; set; }
        public MultipleBlogPostsResponse(List<BlogResponse> blogResponses)
        {
            BlogPosts = blogResponses;
            PostsCount = blogResponses.Count;
        }
    }
}
