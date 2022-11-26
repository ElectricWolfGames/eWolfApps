using System;
using System.Collections.Generic;
using System.Text;

namespace eWolfCommon.Models
{
    public class ExchangeRate
    {
        public string ask { get; set; }
        public string bid { get; set; }
        public string currency { get; set; }
        public string pair { get; set; }
    }
}