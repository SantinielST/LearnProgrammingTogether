namespace LearnProgrammingTogether.ViewModels
{
    public class CreateGroupViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Adress Adress { get; set; }
        public IFormFile Image { get; set; }
        public GroupCategory GroupCategory { get; set; }
        public string AppUserId { get; set; }
    }
}
