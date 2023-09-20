﻿#define ULOGGER_LOGS_ON
#define ULOGGER_WARNINGS_ON
#define ULOGGER_ERRORS_ON

#if ULOGGER_LOGS_OFF || ULOGGER_DISABLE_ALL_LOGS
#undef ULOGGER_LOGS_ON
#endif
#if ULOGGER_WARNINGS_OFF || ULOGGER_DISABLE_ALL_LOGS
#undef ULOGGER_WARNINGS_ON
#endif
#if ULOGGER_ERRORS_OFF || ULOGGER_DISABLE_ALL_LOGS
#undef ULOGGER_ERRORS_ON
#endif

using System;

namespace UnityEngine
{
    internal class UnityLogger : ILogHandler
    {
        private static readonly ULogger _unsortedLogger = ULogger.GetLogger(Tags.Unsorted);

        public void LogException(Exception exception, Object context)
        {
            ULogger.DefaultLogger.LogException(exception, context);
        }

        public void LogFormat(LogType logType, Object context, string format, params object[] args)
        {
            switch (logType)
            {
#if !ULOGGER_LOGS_ON
                case LogType.Log:
                    return;
#endif
#if !ULOGGER_WARNINGS_ON
                case LogType.Warning:
                    return;
#endif
#if !ULOGGER_ERRORS_ON
                case LogType.Error:
                    return;
#endif
            }

            string message;
            if (args == null || args.Length == 0)
            {
                message = format;
            }
            else
            {
                message = string.Format(format, args);
            }
            _unsortedLogger.Log(logType, message, context, 0);
        }
    }
}