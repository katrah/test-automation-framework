using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Framework.Library
{
    public abstract class RequestOptions
    {
        public WebHeaderCollection Headers { get; set; }
    }
}