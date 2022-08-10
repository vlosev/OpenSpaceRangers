using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace Common.Logger
{
    public static class Log
    {
        [Obsolete, Conditional("UNITY_EDITOR")]
        public static void Todo(string logString, Object context = null)
        {
            Debug.LogWarning(logString, context);
        }
    }
}