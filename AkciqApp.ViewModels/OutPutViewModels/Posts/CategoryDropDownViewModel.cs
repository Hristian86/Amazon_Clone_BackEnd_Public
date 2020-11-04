namespace AkciqApp.ViewModels.OutPutViewModels.Posts
{
    using AkciqApp.Mapping;
    using AkciqApp.Models.Models;

    public class CategoryDropDownViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
