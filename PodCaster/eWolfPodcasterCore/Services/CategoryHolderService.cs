using System.Collections.Generic;

namespace eWolfPodcasterCore.Services
{
    // TODO: Need to load and save the categorys as we can add new ones.
    // TODO: Maybe get the starting list from the web site and check for new ones
    public class CategoryHolderService
    {
        public CategoryHolderService()
        {
            Categories.AddRange(new List<string>()
            {
                "Music",
                "Developer",
                "Science",
                "Space",
                "History",
                "Tech",
                "Drama",
                "Nature",
                "Food",
                "SciFi",
                "Gaming",
                "Others",
                "Misc"}
            );
        }

        public static IEnumerable<string> GetAllCategories
        {
            get
            {
                return ServiceLocator.Instance.GetService<CategoryHolderService>().Categories;
            }
        }

        public List<string> Categories { get; } = new List<string>();
    }
}
