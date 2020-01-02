using System.Collections.Generic;

namespace Imobi.Services.Interfaces
{
    public interface IEventTrackerService
    {
        void SendEvent(string eventId);
        void SendEvent(string eventId, string paramName, string value);
        void SendEvent(string eventId, IDictionary<string, string> parameters);
    }
}