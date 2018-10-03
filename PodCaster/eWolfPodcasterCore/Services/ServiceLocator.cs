using eWolfPodcasterCore.Data;
using System;
using System.Collections.Generic;

namespace eWolfPodcasterCore.Services
{
    public class ServiceLocator
    {
        private static ServiceLocator _instance = null;

        private readonly IDictionary<object, object> services;

        private ServiceLocator()
        {
            services = new Dictionary<object, object>
            {
                { typeof(CategoryHolderService), new CategoryHolderService() },
                { typeof(Shows), new Shows() }
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
                return (T)services[typeof(T)];
            }
            catch
            {
                // Fail safe
            }
            return default(T);
        }

        public void SetService<T>(T shows)
        {
            services[typeof(T)] = shows;
        }
    }
}
