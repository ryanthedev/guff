using System;

namespace guff
{
    public interface IGuffLogger<T>
    {
        /// <summary>
        /// Publish a trace event with <paramref name="metaData"/>
        /// </summary>
        /// <param name="metaData"></param>
        void Trace(string message, object metaData);
        void Trace(string message);
        /// <summary>
        /// Publish a debug event with <paramref name="metaData"/>
        /// </summary>
        /// <param name="metaData"></param>
        void Debug(string message, object metaData);
        void Debug(string message);
        /// <summary>
        /// Publish a informational event with <paramref name="metaData"/>
        /// </summary>
        /// <param name="metaData"></param>
        void Info(string message, object metaData);
        void Info(string message);
        /// <summary>
        /// Publish a warning event with <paramref name="metaData"/>
        /// </summary>
        /// <param name="metaData"></param>
        void Warn(string message, object metaData);
        void Warn(string message);
        /// <summary>
        /// Publish a error event with <paramref name="metaData"/>
        /// </summary>
        /// <param name="metaData"></param>
        void Error(string message);
        void Error(string message, object metaData);
        void Error(Exception e, string message, object metaData);
        /// <summary>
        /// Publish a event with <paramref name="metaData"/>
        /// </summary>
        /// <param name="metaData"></param>
        void Pub(string severity, string message);
        void Pub(string severity, string message, object metaData);
    }
}
