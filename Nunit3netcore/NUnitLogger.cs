using System;
using System.IO;
using NUnit.Framework;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Extensions.Logging;
using Serilog.Formatting.Display;

namespace Nunit3netcore
{
    public static class NUnitLogger
    {
        public static Microsoft.Extensions.Logging.ILogger GetLogger()
        {
            var serilog = CreateSerilogLogger();
            return new SerilogLoggerProvider(serilog, dispose: true)
                .CreateLogger(TestContext.CurrentContext.Test.ClassName);
        }

        private static Serilog.ILogger CreateSerilogLogger()
        {
            return new LoggerConfiguration()
                .WriteTo.NUnitOutput()
                .WriteTo.TestContextFile()
                .CreateLogger();
        }

        public static LoggerConfiguration TestContextFile(
            this LoggerSinkConfiguration writeTo)
        {
            var fileName = $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.MethodName}_{DateTime.UtcNow:yy-MM-dd_hh_mm_ss.fff}.txt";
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, fileName);

            FileHelper.CreateFile(filePath);

            TestContext.AddTestAttachment(filePath);
            return writeTo.File(filePath, outputTemplate: "{Message:lj}{NewLine}{Exception}");
        }

        public static LoggerConfiguration NUnitOutput(
            this LoggerSinkConfiguration sinkConfiguration,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            IFormatProvider formatProvider = null,
            LoggingLevelSwitch levelSwitch = null,
            string outputTemplate = "{Message:lj}{NewLine}{Exception}")
        {
            if (sinkConfiguration == null)
                throw new ArgumentNullException(nameof(sinkConfiguration));
            MessageTemplateTextFormatter formatter = new MessageTemplateTextFormatter(outputTemplate, formatProvider);
            return sinkConfiguration.Sink((ILogEventSink)new NUnitSink(formatter), restrictedToMinimumLevel, levelSwitch);
        }
    }
}
