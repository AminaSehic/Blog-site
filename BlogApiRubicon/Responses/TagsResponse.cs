using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApiRubicon.Responses
{
    public class TagsResponse
    {
        public List<string> Tags { get; set; }
        public TagsResponse(List<string> tagResponses)
        {
            Tags = tagResponses;
        }
    }
}
