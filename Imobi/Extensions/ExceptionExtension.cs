using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Imobi.Extensions
{
    public static class ExceptionExtension
    {
        public static string FullException(this Exception exception)
        {
            Exception e = exception;
            StringBuilder stringBuilder = new StringBuilder();

            while (!(e is null))
            {
                stringBuilder.AppendLine("Exception type: " + e.GetType()?.FullName);
                stringBuilder.AppendLine("Message: ");
                stringBuilder.AppendLine(e.Message);
                stringBuilder.AppendLine("Stacktrace:");
                stringBuilder.AppendLine(e.StackTrace);
                stringBuilder.AppendLine();
                e = e.InnerException;
            }

            return stringBuilder.ToString();
        }

        public static StackFrame GetWhereTheExceptionWasGenerated(this Exception exception)
        {
            try
            {
                var stackTrace = new StackTrace(exception, true);
                return stackTrace.GetFrames()?.FirstOrDefault();
            }
            catch { return null; }
        }
    }
}