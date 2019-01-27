using System;
using System.Collections.Generic;
using System.Text;

namespace eWolfCommon.Services
{
    public abstract class ServiceLocatorBase
    {
        protected static ServiceLocatorBase _instance = null;

        protected IDictionary<Type, object> _services;

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
