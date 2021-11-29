using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using eWolfBootstrap.SiteBuilder.Interfaces;

namespace eWolfBootstrap.SiteBuilder

{
    public abstract class PageDetails : ISitePageDetails
    {
        public string FullLocalFilename { get; set; }

        public string RootAddress { get; set; }

        public static List<ISitePageDetails> GetAllPages(Assembly assembly)
        {
            IEnumerable<ISitePageDetails> updates = from t in assembly.GetTypes()
                                                    where t.GetInterfaces().Contains(typeof(ISitePageDetails))
                                                          && t.GetConstructor(Type.EmptyTypes) != null
                                                    select Activator.CreateInstance(t) as ISitePageDetails;

            return updates.Select(x => x).ToList();
        }

        public abstract void CreatePage();
    }
}