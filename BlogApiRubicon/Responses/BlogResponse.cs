using BlogApiRubicon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApiRubicon.Responses
{
    public class BlogResponse
    {
        public String Slug { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String Body { get; set; }
        public List<String> TagList { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public BlogResponse(Blog blog)
        {
            Slug = blog.Slug;
            Title = blog.Title;
            Description = blog.Description;
            Body = blog.Body;
            TagList = blog.Tags.ToList().ConvertAll(t => t.Name);
            CreatedAt = blog.CreatedOn;
            UpdatedAt = blog.UpdatedOn;

        }
    }
}
