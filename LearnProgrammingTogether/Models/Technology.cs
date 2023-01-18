using LearnProgrammingTogether.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnProgrammingTogether.Models
{
    public class Technology
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        [ForeignKey("Adress")]
        public int? AdressId { get; set; }
        public Adress Adress { get; set; }
        public TechnologyCategory TechnologyCategory { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
