using eWolfCommon.Services;
using eWolfPodcasterCore.Data;
using eWolfPodcasterCore.Interfaces;
using eWolfPodcasterCore.Logger;
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
                { typeof(IShows), new Shows() },
                { typeof(ShowLibraryService), new ShowLibraryService() },
                { typeof(LoggerService), new LoggerService() }
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
