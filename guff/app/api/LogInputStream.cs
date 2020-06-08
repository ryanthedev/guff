using guff.app.core.entities;
using System;
using System.Threading;
using System.Threading.Channels;

namespace guff
{
    public class LogInputStream<T> : IGuffLogger<T>
    {
        private static ulong __logIndex;
        private readonly ChannelWriter<LogEvent> _downstream;
        private string _inputSource;

        internal LogInputStream(ChannelWriter<LogEvent> output)
        {
            _downstream = output;
            _inputSource = typeof(T).ToString();
        }

        public void Debug(object metaData)
        {
            writeLog("debug", metaData);
        }

        public void Error(object metaData)
        {
            writeLog("error", metaData);
        }

        public void Info(dynamic metaData)
        {
            writeLog("info", metaData);
        }

        public void Pub(string severity, object metaData)
        {
            writeLog(severity, metaData);
        }

        public void Trace(object metaData)
        {
            writeLog("trace", metaData);
        }

        public void Warn(object metaData)
        {
            writeLog("warn", metaData);
        }

        internal void writeLog(string severity, object metaData)
        {
            var gei = InterlockedEx.Increment(ref __logIndex);
            var evt = new LogEvent()
            {
                GlobalEventIndex = gei,
                EventSource = _inputSource,
                MetaData = metaData,
                ThreadId = Thread.CurrentThread.ManagedThreadId,
                EventTimeStamp = DateTimeOffset.UtcNow,
                Severity = severity
            };
            bool ok;
            do
            {
                ok = _downstream.TryWrite(evt);
                if (!ok)
                    Thread.Sleep(1);
                else
                    break;

                ok = _downstream.TryWrite(evt);
            } while (!ok);
        }

        public void Error(Exception e, object metaData)
        {
            throw new NotImplementedException();
        }
    }
}
