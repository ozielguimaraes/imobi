using Imobi.Services.Interfaces;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Imobi.Services
{
    public class ExceptionService : IExceptionService
    {
        public void TrackError(string messageError)
        {
            Debug.WriteLine("------------- START --------------");
            Debug.WriteLine(messageError);
            Debug.WriteLine("-------------- END -----------------");
            //Crashes.TrackError(null, new Dictionary<string, string> { { "error", messageError } });
        }

        public void TrackError(Exception e, string messageError)
        {
            WriteError(e);
            //Crashes.TrackError(e, new Dictionary<string, string> { { "error", messageError } });
        }

        public void TrackError(Exception e, Dictionary<string, string> properties)
        {
            WriteError(e);
            //Crashes.TrackError(e, properties);
        }

        public void TrackError(Exception ex, string className, string methodName, Dictionary<string, string> properties)
        {
            if (!string.IsNullOrWhiteSpace(className)) properties.Add("Class: ", className);
            if (!string.IsNullOrWhiteSpace(methodName)) properties.Add("Method: ", methodName);
            WriteError(ex, className, methodName);
            properties = properties ?? new Dictionary<string, string>();

            //Crashes.TrackError(ex, properties);
        }

        private void WriteError(Exception ex, string className = null, string methodName = null)
        {
            if (ex is null) return;
            if (!(className is null)) Debug.WriteLine($"Class: {className}");
            if (!(methodName is null)) Debug.WriteLine($"Method: {methodName}");

            var error = ex.Message;
            Debug.WriteLine("------------- START --------------");

            var stackTrace = new StackTrace(ex, true);
            var stackFrame = stackTrace.GetFrame(0);

            if (stackFrame is null)
            {
                if (string.IsNullOrWhiteSpace(ex.Message)) Debug.WriteLine(ex.Message);

                Debug.WriteLine("-------------- END -----------------");
                return;
            }
            var errorFileName = stackFrame.GetFileName();
            if (!string.IsNullOrWhiteSpace(errorFileName)) Debug.WriteLine($"File: {errorFileName}");

            var errorLineNumber = stackFrame.GetFileLineNumber();
            if (errorLineNumber != 0) Debug.WriteLine($"Line: {errorLineNumber}");

            if (!string.IsNullOrWhiteSpace(error)) Debug.WriteLine(error);
            var detailError = ex.InnerException?.Message;
            if (!string.IsNullOrWhiteSpace(detailError))
                if (!detailError.Equals(error))
                    Debug.WriteLine(detailError);

            var detailErrorFurther = ex.InnerException?.InnerException?.Message;
            if (!string.IsNullOrWhiteSpace(detailErrorFurther))
                if (!detailErrorFurther.Equals(error) && !detailErrorFurther.Equals(detailError))
                    Debug.WriteLine(detailErrorFurther);

            Debug.WriteLine("-------------- END -----------------");
        }
    }
}