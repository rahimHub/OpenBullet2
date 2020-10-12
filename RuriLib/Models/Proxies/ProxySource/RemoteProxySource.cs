﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RuriLib.Models.Proxies.ProxySources
{
    public class RemoteProxySource : ProxySource
    {
        public string Url { get; set; } = string.Empty;

        public RemoteProxySource(string url)
        {
            Url = url;
        }

        public override async Task<IEnumerable<Proxy>> GetAll()
        {
            var response = await new HttpClient().GetAsync(Url);
            var content = await response.Content.ReadAsStringAsync();
            var lines = content.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            return lines.Select(l => Proxy.Parse(l, DefaultType, DefaultUsername, DefaultPassword));
        }
    }
}
