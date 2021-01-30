using System.ComponentModel.DataAnnotations;

namespace DevBlog_BlazorServer.Models
{
    public class PersonModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
    }
}