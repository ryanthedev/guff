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

        public void Debug(string message, object metaData)
        {
            writeLog("debug", message, metaData);
        }

        public void Debug(string message)
        {
            Debug(message, null);
        }

        public void Error(string message, object metaData)
        {
            writeLog("error", message, metaData);
        }

        public void Error(Exception e, string message, object metaData)
        {
            writeLog("error", message, metaData, e);
        }

        public void Error(string message)
        {
            Error(message, null);
        }

        public void Info(string message, object metaData)
        {
            writeLog("info", message, metaData);
        }

        public void Info(string message)
        {
            Info(message, null);
        }

        public void Pub(string severity, string message, object metaData)
        {
            writeLog(severity, message, metaData);
        }

        public void Pub(string severity, string message)
        {
            Pub(severity, message, null);
        }

        public void Trace(string message, object metaData)
        {
            writeLog("trace", message, metaData);
        }

        public void Trace(string message)
        {
            Trace(message, null);
        }

        public void Warn(string message, object metaData)
        {
            writeLog("warn", message, metaData);
        }

        public void Warn(string message)
        {
            Warn(message, null);
        }

        internal void writeLog(string severity, string message, object metaData, Exception error = null)
        {
            var gei = InterlockedEx.Increment(ref __logIndex);
            var evt = new LogEvent()
            {
                GlobalEventIndex = gei,
                Message = message,
                Logger = _inputSource,
                MetaData = metaData,
                ThreadId = Thread.CurrentThread.ManagedThreadId,
                EventTimeStamp = DateTimeOffset.UtcNow,
                Severity = severity,
                Error = error
            };
            bool ok;
            do
            {
                ok = _downstream.TryWrite(evt);
                //don't kill the cpu
                if (!ok)
                    _downstream.WaitToWriteAsync().AsTask().Wait();

            } while (!ok);
        }      
    }
}
