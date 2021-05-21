using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApiRubicon.Requests
{
    public class CreateBlogRequest
    {
        public CreateBlogRequestData BlogPost { get; set; }
    }

    public class CreateBlogRequestData
    {
        public String Title { get; set; }
        public String Description { get; set; }
        public String Body { get; set; }
        public List<String> TagList { get; set; }

        public CreateBlogRequestData(string title, string description, string body, List<string> tagList)
        {
            Title = title;
            Description = description;
            Body = body;
            TagList = tagList;
        }
    }
}
