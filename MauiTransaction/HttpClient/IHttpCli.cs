using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiTransaction.Httph
{
    public interface IHttpCli
    {
        HttpClient _cli { get;}
        JsonSerializerOptions _jsonSerializerOptions { get;}
    }
}
