using eWolfCommon.Services;
using eWolfPodcasterCore.Data;
using System;
using System.Collections.Generic;

namespace eWolfPodcasterCore.Services
{
    public class ServiceLocator : ServiceLocatorBase
    {
        private ServiceLocator()
        {
            _services = new Dictionary<Type, object>
            {
                { typeof(CategoryHolderService), new CategoryHolderService() },
                { typeof(Shows), new Shows() },
                { typeof(ShowLibraryService), new ShowLibraryService() }
            };
        }

        public static ServiceLocatorBase Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ServiceLocator();
                }
                return _instance;
            }
        }
    }
}
