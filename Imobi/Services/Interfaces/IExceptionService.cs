using System;
using System.Collections.Generic;

namespace Imobi.Services.Interfaces
{
    public interface IExceptionService
    {
        void TrackError(string messageError);

        void TrackError(Exception ex, string messageError);

        void TrackError(Exception ex, string className, string methodName, Dictionary<string, string> properties = null);

        void TrackError(Exception ex, Dictionary<string, string> properties = null);
    }
}