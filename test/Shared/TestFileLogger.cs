using Microsoft.Framework.Logging;
using System;
using System.IO;

namespace Microsoft.AspNet.Identity.Test
{
    public class TestFileLogger : ILogger
    {
        public string FileName { get; set; }

        public object FileLock { get; private set; } = new object();

        public TestFileLogger(string name)
        {
            FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), (name + "log.txt"));
            if (!File.Exists(FileName))
            {
                File.Create(FileName).Close();
            }
        }

        public IDisposable BeginScope(object state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Write(LogLevel logLevel, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
        {
            lock (FileLock)
            {
                File.AppendAllLines(FileName, new string[] { state.ToString() });
            }
        }
    }
}