using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

namespace Assets.Core.Code.Helper
{
    public static class AnalyticsEvents
    {
        public static void YarnDialogStarted(string dialogName)
        {
            Analytics.CustomEvent("yarn.dialogStart_" + dialogName, new Dictionary<string, object>
            {
                { "name", dialogName },
                { "playtime", Time.time } 
            });
        }

        public static void YarnNodeStarted(string nodeName)
        {
            Analytics.CustomEvent("yarn.nodeStart_" + nodeName, new Dictionary<string, object>
            {
                {"name", nodeName},
                { "playtime", Time.time } 
            });
        }

        public static void YarnNodeCompleted(string nodeName)
        {
            Analytics.CustomEvent("yarn.nodeComplete_" + nodeName, new Dictionary<string, object>
            {
                { "name", nodeName },
                { "playtime", Time.time } 
            });
        }

        public static void YarnDialogCompleted(string dialogName)
        {
            Analytics.CustomEvent("yarn.dialogComplete_" + dialogName, new Dictionary<string, object>
            {
                { "name", dialogName },
                { "playtime", Time.time } 
            });
        }
    }
}
