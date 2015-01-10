using Microsoft.Framework.Logging;
using System;

namespace Microsoft.AspNet.Identity.Test
{
    public class TestFileLoggerFactory : ILoggerFactory
    {

        public void AddProvider(ILoggerProvider provider)
        {

        }

        public ILogger Create(string name)
        {
            return new TestFileLogger(name);
        }

    } 
}
