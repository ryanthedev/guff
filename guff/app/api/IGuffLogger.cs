using System;

namespace guff
{
    public interface IGuffLogger<T>
    {
        /// <summary>
        /// Publish a trace event with <paramref name="metaData"/>
        /// </summary>
        /// <param name="metaData"></param>
        void Trace(object metaData);
        /// <summary>
        /// Publish a debug event with <paramref name="metaData"/>
        /// </summary>
        /// <param name="metaData"></param>
        void Debug(object metaData);
        /// <summary>
        /// Publish a informational event with <paramref name="metaData"/>
        /// </summary>
        /// <param name="metaData"></param>
        void Info(object metaData);
        /// <summary>
        /// Publish a warning event with <paramref name="metaData"/>
        /// </summary>
        /// <param name="metaData"></param>
        void Warn(object metaData);
        /// <summary>
        /// Publish a error event with <paramref name="metaData"/>
        /// </summary>
        /// <param name="metaData"></param>
        void Error(object metaData);
        void Error(Exception e, object metaData);
        /// <summary>
        /// Publish a event with <paramref name="metaData"/>
        /// </summary>
        /// <param name="metaData"></param>
        void Pub(string severity, object metaData);
    }
}
