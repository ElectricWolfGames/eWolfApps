using System;
using System.Collections.Generic;
using eWolfCommon.Services;

namespace eWolfBootstrap.SiteBuilder
{
    public class SiteBuilderServiceLocator : ServiceLocatorBase
    {
        private SiteBuilderServiceLocator()
        {
            _services = new Dictionary<Type, object>
            {
            };
        }

        public static ServiceLocatorBase Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SiteBuilderServiceLocator();
                }
                return _instance;
            }
        }
    }
}