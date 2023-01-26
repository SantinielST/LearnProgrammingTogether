using LearnProgrammingTogether.Models;

namespace LearnProgrammingTogether.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Group> Groups { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
    }
}
