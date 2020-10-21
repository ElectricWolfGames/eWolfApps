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
                new CatergeryData("News"),
                new CatergeryData("Music"),
                new CatergeryData("Music Podcasts"),
                new CatergeryData("Developer"),
                new CatergeryData("Science"),
                new CatergeryData("Other"),
                new CatergeryData("Misc"),
                new CatergeryData("Doctor Who"),
                new CatergeryData("Star wars"),
                new CatergeryData("Entertainment"),
                new CatergeryData("Drama Vintage"),
                new CatergeryData("Drama Sci-fi"),
                new CatergeryData("Drama Others"),
                new CatergeryData("Drama Historical"),
                new CatergeryData("Comedy"),

                new CatergeryData("History"),
                new CatergeryData("Country"),
                new CatergeryData("Gaming"),
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