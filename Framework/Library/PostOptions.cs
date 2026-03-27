using System;
using System.Collections.Generic;
using System.Net;
using Framework.Library;

namespace Framework.Library
{
    public class PostOptions : RequestOptions
    {
        public bool FullEncode { get; set; } = false;

        public uint NumberOfTries { get; set; } = 1;

        public CookieContainer CookieContainer { get; set; }

        public PostType BodyType { get; set; } = PostType.Json;

        public string ContentTypeOverride { get; set; }
    }
}