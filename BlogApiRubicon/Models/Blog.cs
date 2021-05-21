using BlogApiRubicon.Requests;
using Slugify;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlogApiRubicon.Models
{
    [Table("Blog")]
    public class Blog : BaseEntity

    {
        private String _title;
        private readonly SlugHelper slugHelper = new SlugHelper();
        [Key]
        [Required]
        public int Id { get; set; }
        [StringLength(500)]
        [Required]
        public String Title
        {
            get { return _title; }
            set
            {
                _title = value;
                Slug = slugHelper.GenerateSlug(value);
            }
        }
        [StringLength(500)]
        [Required]
        public String Slug { get; private set; }
        [StringLength(500)]
        [Required]
        public String Description { get; set; }
        [StringLength(500)]
        [Required]
        public String Body { get; set; }
        [JsonIgnore]
        public virtual ICollection<Tag> Tags { get; set; }

        [JsonConstructor]
        public Blog(CreateBlogRequestData blogRequestData)
        {
            Title = blogRequestData.Title;
            Description = blogRequestData.Description;
            Body = blogRequestData.Body;
        }
        [JsonConstructor]
        public Blog()
        {

        }

        public override string ToString()
        {
            var tags = Tags.ToList().ConvertAll(t => $"{t.Id}, {t.Name}");
            return $"id: {Id}, title: {Title}, description: {Description}, body: {Body}, tags: {tags}";
        }
    }
}
