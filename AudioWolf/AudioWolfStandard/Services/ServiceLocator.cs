using AudioWolfStandard.Options;
using System.Collections.Generic;

namespace AudioWolfStandard.Services
{
    public class ServiceLocator
    {
        private static ServiceLocator _instance = null;

        private readonly IDictionary<object, object> services;

        private ServiceLocator()
        {
            services = new Dictionary<object, object>
            {
                { typeof(OptionsHolder), new OptionsHolder() },
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
    }
}
