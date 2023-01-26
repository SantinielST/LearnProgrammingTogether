using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnProgrammingTogether.Models
{
    public class AppUser : IdentityUser
    {
        public string NickName { get; set; }
        public string? StudyLang { get; set; }
        public string? Level { get; set; }
        public string? TypeFramework { get; set; }
        public string? ProfileImageUrl { get; set; }  
        [ForeignKey("Adress")]
        public int? AdressId { get; set; }
        public Adress? Adress { get; set; }

        public ICollection<Group> Groups { get; set; }
        public ICollection<Technology> Technologies { get; set; }
    }
}
