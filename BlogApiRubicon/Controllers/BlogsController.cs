using BlogApiRubicon.DAL;
using BlogApiRubicon.Exceptions;
using BlogApiRubicon.Models;
using BlogApiRubicon.Requests;
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
    [Route("api/posts")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public BlogsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/posts
        [HttpGet]
        public async Task<ActionResult<MultipleBlogPostsResponse>> GetBlogs()
        {
            string tag = Request.Query["tag"].ToString();
            List<Blog> blogs = new List<Blog>();
            if (tag.Length != 0)
            {
                await _context.Blog
                     .Include("Tags")
                     .Where(blog => blog.Tags
                         .Select(t => t.Name)
                         .Contains(tag))
                     .ToListAsync();
            }
            else
            {
                blogs = await _context.Blog.Include("Tags").ToListAsync();
            }
            var blogResponses = blogs.ConvertAll(blog => new BlogResponse(blog));
            return new MultipleBlogPostsResponse(blogResponses);
        }

        // GET: api/posts/slug
        [HttpGet("{slug}")]
        public async Task<ActionResult<SingleBlogPostResponse>> GetBlog(string slug)
        {
            var blog = await _context.Blog.Include("Tags").Where(b => b.Slug == slug).FirstOrDefaultAsync();
            if (blog == null)
            {
                return UnprocessableEntity(new ClientErrorResponse("No blogs found for this slug"));
            }
            return new SingleBlogPostResponse(new BlogResponse(blog));
        }

        // PUT: api/posts/slug
        [HttpPut("{slug}")]
        public async Task<IActionResult> PutBlog(string slug, UpdateBlogRequest blogUpdateRequest)
        {
            try
            {
                Blog blog = await _context.Blog.Include("Tags").Where(b => b.Slug == slug).FirstOrDefaultAsync();
                blog.Title = blogUpdateRequest.BlogPost.Title;
                _context.Entry(blog).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Created($"api/posts/{blog.Slug}", new SingleBlogPostResponse(blog));
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new ClientErrorResponse("Couldn't update blog."));
            }
            catch (NullReferenceException)
            {
                return UnprocessableEntity(new ClientErrorResponse("One or more attributes are invalid"));
            }
            catch (Exception e)
            {
                return BadRequest(new ClientErrorResponse(e.Message));
            }

        }

        // POST: api/posts
        [HttpPost]
        public async Task<IActionResult> PostBlogs(CreateBlogRequest blogRequest)
        {
            try
            {
                Blog blog = new Blog(blogRequest.BlogPost);
                List<string> tagList = blogRequest.BlogPost.TagList;
                List<Tag> tagsForBlog = new List<Tag>();

                SlugHelper slugHelper = new SlugHelper();
                String newSlug = slugHelper.GenerateSlug(blogRequest.BlogPost.Title);
                if (_context.Blog.Any(b => b.Slug == newSlug))
                {
                    return BadRequest(new ClientErrorResponse("Please enter a new title"));
                }

                foreach (string tag in tagList)
                {
                    Tag t = _context.Tag.Where(t => t.Name == tag).FirstOrDefault();
                    if (t == null)
                    {
                        t = new Tag(tag);
                    }
                    tagsForBlog.Add(t);
                }

                blog.Tags = tagsForBlog;
                _context.Blog.Add(blog);
                await _context.SaveChangesAsync();
                return Created($"api/posts/{blog.Slug}", new SingleBlogPostResponse(blog));
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new ClientErrorResponse("Couldn't save blog."));
            }
            catch (NullReferenceException)
            {
                return UnprocessableEntity(new ClientErrorResponse("One or more attributes are invalid"));
            }
            catch (Exception e)
            {
                return BadRequest(new ClientErrorResponse(e.Message));
            }
        }

        // DELETE: api/posts/slug
        [HttpDelete("{slug}")]
        public async Task<ActionResult<Blog>> DeleteBlog(string slug)
        {
            if (slug == null)
            {
                return BadRequest(new ClientErrorResponse($"Slug {slug} doesn't exist"));
            }
            var blog = await _context.Blog.Include("Tags").Where(b => b.Slug == slug).FirstOrDefaultAsync();

            if (blog == null)
            {
                return NotFound(new ClientErrorResponse($"No blog with slug name {slug}"));
            }
            try
            {
                _context.Blog.Remove(blog);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return NotFound(new ClientErrorResponse(e.Message));
            }
            return blog;
        }
    }
}
