using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace IItau.MX4.Logger.Providers.Splunk.Extensions
{
    internal static class StringExtensions
    {
        public static T Deserialize<T>(this string json)
        {
            if (string.IsNullOrEmpty(json))
                return default;

            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
