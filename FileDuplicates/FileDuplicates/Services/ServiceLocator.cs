using eWolfCommon.Services;
using System;
using System.Collections.Generic;

namespace FileDuplicates.Services
{
    public class ServiceLocator : ServiceLocatorBase
    {
        private ServiceLocator()
        {
            _services = new Dictionary<Type, object>
            {
                 { typeof(FileDetailsHolderService), new FileDetailsHolderService() },
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