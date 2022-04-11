using System;
namespace Itau.MX4.Logger.Providers.Splunk.Extensions
{
    internal static class DateTimeExtensions
    {
        public static ulong ToUnixTimeSeconds(this  DateTime dt)
        {
            var dtOffset = new DateTimeOffset(dt);
            var epochSeconds = dtOffset.ToUnixTimeSeconds();

            return (ulong)epochSeconds;
        }
    }
}
