using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Library
{
    public enum PostType
    {
        Json,
        UrlEncoded,
        MultipartFile,
        GraphQl,
        None
    }
}