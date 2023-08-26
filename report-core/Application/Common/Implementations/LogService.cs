using Microsoft.Extensions.Options;
using report_core.Application.Common.Services;
using report_core.Domain.Entities.Common;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace report_core.Application.Common.Implementations
{
    public class LogService : ILogService
    {
        private readonly AppSetings _appSettings;
        private readonly string Logpath;
        private const string logSeparator = "------------------------------------------------------";
        public LogService(IOptions<AppSetings> appSettings)
        {
            _appSettings = appSettings.Value;
            Logpath = $"{_appSettings.Logpath}\\{DateTime.Now.Date.ToString("yyyyyMMdd")}";
        }
        public void LogwriteInfo(string message, string filename)
        {
            string path = $"{Logpath}\\{filename}.log";
            using (var log = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File(path).CreateLogger())
            {
                log.Information(Environment.NewLine + message + Environment.NewLine + logSeparator + logSeparator);
            }
        }
        public void LogwriteError(string message, string filename)
        {
            string path = $"{Logpath}\\{filename}.log";
            using (var log = new LoggerConfiguration().MinimumLevel.Error().WriteTo.File(path).CreateLogger())
            {
                log.Error(Environment.NewLine + message + Environment.NewLine + logSeparator + logSeparator);
            }
        }
        public void LogwriteWarning(string message, string filename)
        {
            string path = $"{Logpath}\\{filename}.log";

            using (var log = new LoggerConfiguration().MinimumLevel.Warning().WriteTo.File(path).CreateLogger())
            {
                log.Warning(Environment.NewLine + message + Environment.NewLine + logSeparator + logSeparator);
            }
        }

    }
}
