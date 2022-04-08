using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Itau.MX4.Logger.Providers.Splunk.Extensions
{
    internal static class ObjectExtensions
    {
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions { IgnoreNullValues = true, WriteIndented = false };

        public static string Serialize(this object obj)
        {
            if (obj == null)
                return null;

            return JsonSerializer.Serialize(obj, _jsonSerializerOptions);
        }
    }
}
