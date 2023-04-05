using eWolfPodcasterCore.Library;
using System.Collections.Generic;

namespace eWolfPodcasterCore.Services
{
    public class CategoryHolderService
    {
        public CategoryHolderService()
        {
            Categories = new List<CatergeryData>
            {
                new CatergeryData("Drama Sci-fi Vintage"),
                new CatergeryData("Drama Sci-fi New"),
                new CatergeryData("Drama Sci-fi Other"),

                new CatergeryData("Comedy"),
                new CatergeryData("Country"),
                new CatergeryData("Developer"),
                new CatergeryData("Doctor Who"),
                new CatergeryData("Drama Historical"),
                new CatergeryData("Drama Others"),
                new CatergeryData("Drama Sci-fi"),
                new CatergeryData("Drama Vintage"),
                new CatergeryData("Entertainment"),
                new CatergeryData("Gaming"),
                new CatergeryData("History"),
                new CatergeryData("Misc"),
                new CatergeryData("Music Podcasts"),
                new CatergeryData("Music"),
                new CatergeryData("Music|StarWars"),
                new CatergeryData("Nature"),
                new CatergeryData("News"),
                new CatergeryData("Other"),
                new CatergeryData("Science"),
                new CatergeryData("Star wars"),
                new CatergeryData("Tech"),
                new CatergeryData("Trains")
            };
        }

        public static List<CatergeryData> GetAllCategories
        {
            get
            {
                return ServiceLocator.Instance.GetService<CategoryHolderService>().Categories;
            }
        }

        public List<CatergeryData> Categories { get; }
    }
}