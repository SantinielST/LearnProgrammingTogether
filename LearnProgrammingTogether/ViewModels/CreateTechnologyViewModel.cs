using LearnProgrammingTogether.Data.Enum;

namespace LearnProgrammingTogether.ViewModels
{
    public class CreateTechnologyViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public Adress Adress { get; set; }
        public TechnologyCategory TechnologyCategory { get; set; }
    }
}
