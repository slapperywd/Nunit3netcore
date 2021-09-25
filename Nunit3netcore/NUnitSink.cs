using System;
using System.IO;
using NUnit.Framework;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Display;

namespace Nunit3netcore
{
    public class NUnitSink : ILogEventSink
    {
        private readonly MessageTemplateTextFormatter _formatter;

        public NUnitSink(MessageTemplateTextFormatter formatter) => this._formatter = formatter != null ? formatter : throw new ArgumentNullException(nameof(formatter));

        public void Emit(LogEvent logEvent)
        {
            if (logEvent == null)
                throw new ArgumentNullException(nameof(logEvent));
            if (TestContext.Out == null)
                return;
            StringWriter stringWriter = new StringWriter();
            this._formatter.Format(logEvent, (TextWriter)stringWriter);
            ((TextWriter)TestContext.Out).Write(stringWriter.ToString());
        }
    }
}
