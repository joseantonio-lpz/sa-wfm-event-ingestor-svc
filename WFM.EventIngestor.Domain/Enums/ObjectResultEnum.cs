namespace WFM.EventIngestor.Domain.Enums
{
    public static class ObjectResultEnum
    {
        public enum LogEvent
        {
            TRACE,
            DEBUG,
            INFO,
            WARN,
            ERROR,
            FATAL
        }

         public enum LogLevel
        {
            Trace,
            Debug,
            Info,
            Warn,
            Error,
            Fatal
        }
    }
}
