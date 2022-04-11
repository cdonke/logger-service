using System;
using Microsoft.Extensions.Logging;

namespace Itau.MX4.Logger.Domain.Interfaces
{
    public interface ILoggerFormatter<TOutput>
    {
        /// <summary>
        /// Format the specified logLevel, eventId, state and exception into log string entry.
        /// </summary>
        /// <returns>Formatted log string.</returns>
        /// <param name="categoryName">Log category name.</param>
        /// <param name="logLevel">Log level.</param>
        /// <param name="eventId">Event identifier.</param>
        /// <param name="state">Log object state.</param>
        /// <param name="exception">Log exception.</param>
        /// <typeparam name="T">Log entry.</typeparam>
        string Format<T>(string categoryName, LogLevel logLevel, EventId eventId, T state, Exception exception);

        /// <summary>
        /// Formats the specified logLevel, eventId, state and exception into json entry.
        /// </summary>
        /// <returns>The json.</returns>
        /// <param name="categoryName">Log category name.</param>
        /// <param name="logLevel">Log level.</param>
        /// <param name="eventId">Event identifier.</param>
        /// <param name="state">Log object state.</param>
        /// <param name="exception">Log exception.</param>
        /// <typeparam name="T">Log entry.</typeparam>
        TOutput FormatJson<T>(string categoryName, LogLevel logLevel, EventId eventId, T state, Exception exception);

    }
}
