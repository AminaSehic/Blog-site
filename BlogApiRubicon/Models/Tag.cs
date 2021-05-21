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
    [Table("Tag")]
    public class Tag
    {
        private readonly SlugHelper slugHelper = new SlugHelper();
        private String _name;


        [Key]
        [Required]
        public int Id { get; set; }
        [StringLength(500)]
        [Required]
        public String Slug { get; private set; }
        [StringLength(500)]
        [Required]
        public String Name
        {
            get { return _name; }
            set
            {
                _name = value;
                Slug = slugHelper.GenerateSlug(value);
            }
        }
        [JsonIgnore]
        public virtual ICollection<Blog> Blogs { get; set; }

        public Tag(string name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return $"{this.Id}, {this.Name}";
        }
    }
}
