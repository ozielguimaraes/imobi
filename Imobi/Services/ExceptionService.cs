using Imobi.Extensions;
using Imobi.Services.Interfaces;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Imobi.Services
{
    public class ExceptionService : IExceptionService
    {
        public void TrackError(string messageError)
        {
            if (string.IsNullOrWhiteSpace(messageError)) return;

            WriteError(messageError);

#if (!DEBUG)
            Crashes.TrackError(null, new Dictionary<string, string> { { "error", messageError } });
#endif
        }

        public void TrackError(Exception ex, string messageError)
        {
            if (!string.IsNullOrWhiteSpace(messageError)) Debug.WriteLine(messageError);
            WriteError(ex);

#if (!DEBUG)
            Crashes.TrackError(ex, new Dictionary<string, string> { { "error", messageError } });
#endif
        }

        public void TrackError(Exception ex, Dictionary<string, string> properties)
        {
            WriteError(ex);
#if (!DEBUG)
            Crashes.TrackError(ex, properties);
#endif
        }

        public void TrackError(Exception ex, string className, string methodName, Dictionary<string, string> properties)
        {
            if (!string.IsNullOrWhiteSpace(methodName))
            {
                if (properties is null) properties = new Dictionary<string, string>();
                properties.Add("Method: ", methodName);
            }
            WriteError(ex, className, methodName);

#if (!DEBUG)
            Crashes.TrackError(ex, properties);
#endif
        }

        private void WriteError(string messageError)
        {
            WriteStartLog();
            WriteIfNotNull(messageError);
            WriteEndLog();
        }

        private void WriteError(Exception ex, string className = null, string methodName = null)
        {
            if (ex is null) return;

            WriteStartLog();

            var stackFrame = ex.GetWhereTheExceptionWasGenerated();
            if (stackFrame is null)
            {
                if (!string.IsNullOrWhiteSpace(className)) Write($"Class: {className}");
                if (!string.IsNullOrWhiteSpace(methodName)) Write($"Method: {methodName}");
                if (!string.IsNullOrWhiteSpace(ex.Message)) WriteIfNotNull(ex.Message);

                WriteEndLog();
                return;
            }

            var errorFileName = stackFrame.GetFileName();
            if (!string.IsNullOrWhiteSpace(errorFileName)) Write($"FileName: {errorFileName}");

            if (!string.IsNullOrWhiteSpace(className)) Write($"Class: {className}");
            else
            {
                var classFullName = stackFrame.GetMethod().DeclaringType.FullName;
                if (!string.IsNullOrWhiteSpace(classFullName)) Write($"Class: {classFullName}");
            }

            if (!string.IsNullOrWhiteSpace(methodName)) Write(methodName);
            else
            {
                var method = stackFrame.GetMethod()?.Name;
                if (string.IsNullOrWhiteSpace(method)) Write($"Method: {method}");
            }

            var errorLineNumber = stackFrame.GetFileLineNumber();
            if (errorLineNumber != 0) Write($"Line: {errorLineNumber}");

            var columnNumber = stackFrame.GetFileColumnNumber();
            Write($"Column: {columnNumber}");

            WriteIfNotNull(ex.FullException());
            WriteEndLog();
        }

        private void WriteStartLog()
        {
            WriteIfNotNull("------------- START --------------");
        }

        private void WriteEndLog()
        {
            WriteIfNotNull("-------------- END -----------------");
        }

        private void WriteIfNotNull(string message)
        {
            if (message is null) return;

            Write(message);
        }

        private void Write(string message)
        {
            Debug.WriteLine(message);
        }
    }
}