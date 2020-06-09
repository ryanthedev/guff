using System;

namespace guff.app.core.entities
{
    public class LogEvent
    {
        public ulong GlobalEventIndex { get; set; }
        public DateTimeOffset EventTimeStamp { get; set; }
        public string Severity { get; set; }
        public string Logger { get; set; }
        public int ThreadId { get; set; }
        public string Message { get; set; }
        public object MetaData { get; set; }
        public Exception Error { get; set; }
    }
}
