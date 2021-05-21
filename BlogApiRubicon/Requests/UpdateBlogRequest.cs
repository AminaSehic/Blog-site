using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApiRubicon.Requests
{
    public class UpdateBlogRequest
    {
        public UpdateBlogRequestData BlogPost { get; set; }
    }

    public class UpdateBlogRequestData
    {
        public String Title { get; set; }
    }
}
