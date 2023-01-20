using LearnProgrammingTogether.Data.Enum;

namespace LearnProgrammingTogether.ViewModels
{
    public class EditTechnologyViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string? Url { get; set; }
        public TechnologyCategory TechnologyCategory { get; set; }
        public int AdressId { get; set; }
        public Adress Address { get; set; }
    }
}
