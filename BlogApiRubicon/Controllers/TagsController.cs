using BlogApiRubicon.DAL;
using BlogApiRubicon.Models;
using BlogApiRubicon.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Slugify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApiRubicon.Controllers
{
    [Route("api/tags")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly SlugHelper slugHelper;

        public TagsController(ApplicationDBContext context)
        {
            _context = context;
            slugHelper = new SlugHelper();
        }

        [HttpGet]
        public async Task<ActionResult<TagsResponse>> GetTag()
        {
            var tags = await _context.Tag.ToListAsync();
            var tagResponses = tags.ConvertAll(tag => tag.Name);
            return new TagsResponse(tagResponses);
        }


    }
}
