using System;
using System.Collections.Generic;
using Android.OS;
using Firebase.Analytics;
using Xamarin.Forms;
using Imobi.Services.Interfaces;
using Imobi.Droid.Services;

[assembly: Dependency(typeof(EventTrackerService))]
namespace Imobi.Droid.Services
{
    public class EventTrackerService : IEventTrackerService
    {
        public void SendEvent(string eventId)
        {
            var firebaseAnalytics = FirebaseAnalytics.GetInstance(Android.App.Application.Context);
            firebaseAnalytics.LogEvent(eventId, null);
        }

        public void SendEvent(string eventId, string paramName, string value)
        {
            SendEvent(eventId, new Dictionary<string, string>
            {
                {paramName, value}
            });
        }

        public void SendEvent(string eventId, IDictionary<string, string> parameters)
        {
            var firebaseAnalytics = FirebaseAnalytics.GetInstance(Android.App.Application.Context);

            var bundle = new Bundle();
            foreach (var param in parameters)
            {
                bundle.PutString(param.Key, param.Value);
            }

            firebaseAnalytics.LogEvent(eventId, bundle);
        }
    }
}