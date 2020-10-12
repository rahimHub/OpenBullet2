﻿using RuriLib.Functions.Http;
using RuriLib.Models.Blocks.Parameters;
using System.Collections.Generic;

namespace RuriLib.Models.Blocks.Custom
{
    public class HttpRequestBlockDescriptor : BlockDescriptor
    {
        public HttpRequestBlockDescriptor()
        {
            Id = "HttpRequest";
            Name = Id;
            Description = "Performs an Http request and reads the response";
            Category = new BlockCategory
            {
                Name = "Http",
                BackgroundColor = "#32cd32",
                ForegroundColor = "#000",
                Path = "RuriLib.Blocks.Requests.Http",
                Namespace = "RuriLib.Blocks.Requests.Http.Methods",
                Description = "Blocks for performing Http requests"
            };
            Parameters = new BlockParameter[]
            {
                new StringParameter { Name = "url", DefaultValue = "https://google.com" },
                new EnumParameter { Name = "method", EnumType = typeof(HttpMethod), DefaultValue = HttpMethod.GET.ToString() },
                new BoolParameter { Name = "autoRedirect", DefaultValue = true },
                new EnumParameter { Name = "securityProtocol", EnumType = typeof(SecurityProtocol), DefaultValue = SecurityProtocol.SystemDefault.ToString() },
                new DictionaryOfStringsParameter { Name = "customCookies", InputMode = Settings.SettingInputMode.Interpolated,
                    DefaultValue = new Dictionary<string, string>() },
                new DictionaryOfStringsParameter { Name = "customHeaders", InputMode = Settings.SettingInputMode.Interpolated,
                    DefaultValue = new Dictionary<string, string>
                    {
                        { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.116 Safari/537.36" },
                        { "Pragma", "no-cache" },
                        { "Accept", "*/*" },
                        { "Accept-Language", "en-US,en;q=0.8" }
                    }
                },
                new IntParameter { Name = "timeoutMilliseconds", DefaultValue = 10000 },
                new StringParameter { Name = "httpVersion", DefaultValue = "1.1" }
            };
        }
    }
}
