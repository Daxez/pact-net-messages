using System;
using System.Linq;
using PactNetMessages.Logging;


namespace PactNetMessages.Infrastructure.Logging
{
    internal class LocalLogger : IDisposable
    {
        private readonly ILocalLogMessageHandler _logHandler;

        internal string LogPath { get { return _logHandler.LogPath; } }

        public LocalLogger(string logFilePath)
        {
            _logHandler = new LocalRollingLogFileMessageHandler(logFilePath);
        }

        public bool Log(LogLevel logLevel, Func<string> messageFunc, Exception exception,
            params object[] formatParameters)
        {
            //Handles the log level enabled checks
            if (messageFunc == null &&
                exception == null &&
                (formatParameters == null || !formatParameters.Any()))
            {
                return true;
            }

            _logHandler.Handle(new LocalLogMessage(logLevel, messageFunc, exception, formatParameters));

            return true;
        }

        public void Dispose()
        {
            if (_logHandler != null)
            {
                _logHandler.Dispose();
            }
        }
    }
}