using eWolfPodcasterCore.Library;
using System.Collections.Generic;

namespace eWolfPodcasterCore.Services
{
    public class CategoryHolderService
    {
        public CategoryHolderService()
        {
            Categories = new List<CatergeryData>();
            Categories.Add(new CatergeryData("News"));
            Categories.Add(new CatergeryData("Music"));
            Categories.Add(new CatergeryData("Music Podcasts"));
            Categories.Add(new CatergeryData("Developer"));
            Categories.Add(new CatergeryData("Science"));
            Categories.Add(new CatergeryData("Other"));
            Categories.Add(new CatergeryData("Misc"));
            Categories.Add(new CatergeryData("Doctor Who"));
            Categories.Add(new CatergeryData("Star wars"));
            Categories.Add(new CatergeryData("Entertainment"));
            Categories.Add(new CatergeryData("Drama Vintage"));
            Categories.Add(new CatergeryData("Drama Sci-fi"));
            Categories.Add(new CatergeryData("Drama Others"));
            Categories.Add(new CatergeryData("Drama Historical"));
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
