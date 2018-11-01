using eWolfPodcasterCore.Data;
using System;
using System.Collections.Generic;

namespace eWolfPodcasterCore.Services
{
    public class ServiceLocator
    {
        private static ServiceLocator _instance = null;

        private readonly IDictionary<Type, object> _services;

        private ServiceLocator()
        {
            _services = new Dictionary<Type, object>
            {
                { typeof(CategoryHolderService), new CategoryHolderService() },
                { typeof(Shows), new Shows() },
                { typeof(ShowLibraryService), new ShowLibraryService() }
            };
        }

        public static ServiceLocator Instance
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

        public T GetService<T>()
        {
            try
            {
                return (T)_services[typeof(T)];
            }
            catch
            {
                // Fail safe
            }
            return default(T);
        }

        public void InjectService<T>(object service)
        {
            Type t2 = typeof(T);
            _services[t2] = service;
        }
    }
}
