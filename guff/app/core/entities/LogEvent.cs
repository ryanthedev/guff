using System;

namespace guff.app.core.entities
{
    public class LogEvent
    {
        public ulong GlobalEventIndex { get; set; }
        public DateTimeOffset EventTimeStamp { get; set; }
        public string Severity { get; set; }
        public string EventSource { get; set; }
        public int ThreadId { get; set; }
        public object MetaData { get; set; }
    }
}
