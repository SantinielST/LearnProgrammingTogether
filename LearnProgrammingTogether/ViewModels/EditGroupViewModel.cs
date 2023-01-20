namespace LearnProgrammingTogether.ViewModels
{
    public class EditGroupViewModel
    {
        public int Id{ get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string? Url { get; set; }
        public GroupCategory GroupCategory { get; set; }
        public int AdressId { get; set; }
        public Adress Adress { get; set; }
    }
}
