using System.ComponentModel.DataAnnotations;

namespace LearnProgrammingTogether.Models
{
    public class AppUser /*: IdentityUser*/
    {
        [Key]
        public string Id { get; set; }
        public string NickName { get; set; }
        public string? StudyLang { get; set; }
        public string? Level { get; set; }
        public string? TypeFramework { get; set; }
        public Adress? Adress { get; set; }

        public ICollection<Group> Groups { get; set; }

        public ICollection<Technology> Technologies { get; set; }
    }
}
