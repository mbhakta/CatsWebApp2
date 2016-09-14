using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace CatsWebApp2.Utilities
{
    /// <summary>
    /// The event logger
    /// </summary>
    internal sealed class Logger
    {
        /// <summary>
        /// Logging level
        /// </summary>
        [Flags]
        public enum LogLevel
        {
            /// <summary>
            /// Debug level 
            /// </summary>
            Debug = 0,
            /// <summary>
            /// Information level
            /// </summary>
            Info,
            /// <summary>
            /// Warnings only
            /// </summary>
            Warning,
            /// <summary>
            /// Fatal errors/exceptions only
            /// </summary>
            Error
        }

        /// <summary>
        /// Category of event log
        /// </summary>
        [Flags]
        public enum Category
        {
            Generic = 0,        // wildcard
            Service,            // indicating the system is back up again
            ElevatedLog,        // indicating the system is going into warning or error mode
        }

        // Create a logger for use in this class
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Log a line
        /// </summary>
        /// <param name="message">the string</param>
        /// <param name="logLevel">log level</param>
        public static void WriteLine(string message, LogLevel logLevel = LogLevel.Debug)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    Log.Debug(message);
                    break;
                case LogLevel.Error:
                    Log.Error(message);
                    break;
                case LogLevel.Warning:
                    Log.Warn(message);
                    break;
                case LogLevel.Info:
                    Log.Info(message);
                    break;
            }
        }

        /// <summary>
        /// Write the exception to the log
        /// </summary>
        /// <param name="exception">the exception</param>
        /// <param name="logLevel">log level</param>
        public static void WriteLine(Exception exception, LogLevel logLevel = LogLevel.Debug)
        {
#if DEBUG
            WriteLine(exception.ToString(), logLevel);
#else
            WriteLine(exception.Message, logLevel);
#endif
        }

        /// <summary>
        /// Write to Windows Event log
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logLevel"></param>
        /// <param name="category"></param>
        public static void WriteEventLog(string message,
            EventLogEntryType logLevel = EventLogEntryType.Information, Category category = Category.Generic)
        {
            var src = "CatsWebApp2";
            var log = "Application";
            var eventid = Convert.ToInt32(ConfigurationManager.AppSettings["EventlogId"]);
            if (!EventLog.SourceExists(src))
            {
                EventLog.CreateEventSource(src, log);
            }

            EventLog.WriteEntry(src, message, logLevel, eventid, (short)category, null);
        }
    }
}