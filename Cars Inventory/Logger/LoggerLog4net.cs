using log4net;
using System.Reflection;

namespace Cars_Inventory.Logger
{
    public sealed class LoggerLog4net : ILogger
    {
        private static readonly Lazy<LoggerLog4net> _loggerInstance = new Lazy<LoggerLog4net>(() => new LoggerLog4net());
        private readonly ILog _logger;
        public LoggerLog4net()
        {
            this._logger = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);
        }
        /// <summary>
        /// Gets the Logger instance.
        /// </summary>
        public static LoggerLog4net Instance
        {
            get { return _loggerInstance.Value; }
        }

        /// <summary>
        /// Logs a message object with the Debug level.
        /// </summary>
        /// <param name="message"></param>
        public void Debug(object message)
        {
            if (_logger.IsDebugEnabled)
                _logger.Debug(message);
        }

        /// <summary>
        /// Logs a message object with the Info level.
        /// </summary>
        /// <param name="message"></param>
        public void Info(object message)
        {
            if (_logger.IsInfoEnabled)
                _logger.Info(message);
        }

        /// <summary>
        /// Logs a message object with the Error level.
        /// </summary>
        /// <param name="message"></param>
        public void Error(object message)
        {
            _logger.Error(message);
        }

        /// <summary>
        /// Logs a message object with the Debug level including the exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Debug(object message, Exception exception)
        {
            if (_logger.IsDebugEnabled)
                _logger.Debug(message, exception);
        }

        /// <summary>
        /// Logs a message object with the Info level including the exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Info(object message, Exception exception)
        {
            if (_logger.IsInfoEnabled)
                _logger.Info(message, exception);
        }

        /// <summary>
        /// Logs a message object with the Error level including the exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Error(object message, Exception exception)
        {
            _logger.Error(message, exception);
        }
    }
}
