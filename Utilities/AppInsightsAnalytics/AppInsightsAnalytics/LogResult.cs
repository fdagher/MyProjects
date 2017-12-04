using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppInsightsAnalytics
{
    public class LogResult
    {
        public string SessionId { get; set; }
        public string Timestamp { get; set; }
        public string Message { get; set; }
        public LogLevel SeverityLevel { get; set; }
        public string CorrelationId { get; set; }
        public string SourceName { get; set; }
        public string SourceLocation { get; set; }
        public string UserId { get; set; }
        public string RequestType { get; set; }
        public bool? IsMonitor { get; set; }
        public string Details { get; set; }
    }

    public enum LogLevel
    {
        Trace = 0,
        Information = 1,
        Warning = 2,
        Error = 3,
        Critical = 4,
        None = 5,
        Monitoring = 6
    }
}
