using NLog;

namespace DAL.Logger
{
    public class LoggerAdapter: ILogger
    {
        private static readonly object _syncObj = new object();

        private static readonly NLog.Logger InnerLogger = LogManager.GetCurrentClassLogger();

        private static LoggerAdapter _loggerInstance;

        public static LoggerAdapter GetLogger()
        {
            if (_loggerInstance == null)
                lock (_syncObj)
                    if (_loggerInstance == null)
                        _loggerInstance = new LoggerAdapter();
            return _loggerInstance;
        }

        public void Trace(string message) => InnerLogger.Trace(message);

        public void Debug(string message) => InnerLogger.Debug(message);

        public void Info(string message) => InnerLogger.Info(message);

        public void Warn(string message) => InnerLogger.Warn(message);

        public void Error(string message) => InnerLogger.Error(message);

        public void Fatal(string message) => InnerLogger.Fatal(message);
    }
}