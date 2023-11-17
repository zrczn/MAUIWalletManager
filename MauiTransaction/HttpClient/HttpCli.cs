using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiTransaction.Httph
{
    public class HttpCli : IHttpCli
    {
        public HttpClient _cli { get; private set; }

        public JsonSerializerOptions _jsonSerializerOptions { get; private set;}

        public HttpCli()
        {
            _cli = new HttpClient();
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
    }
}
