using System.ComponentModel.DataAnnotations;

namespace Auth.Entities
{
    public class Post
    {
        [Key]

        public Guid PostId { get; set; }


        [Required]
        public string title { get; set; }


        public string description { get; set; }

        [Required]
        public Guid userId { get; set; }

        [Required]
        public User user { get; set; }
    }
}
