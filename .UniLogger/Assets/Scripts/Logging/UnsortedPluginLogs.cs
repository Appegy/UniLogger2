﻿using UnityEngine;

namespace Appegy.UniLogger.Example
{
    public class UnsortedPluginLogs : MonoBehaviour
    {
        private void Start()
        {
            CallLogsInMethod();
        }

        private void CallLogsInMethod()
        {
            ULogger.Log("Unsorted log (ULogger)", this);
            ULogger.LogWarning("Unsorted warning (ULogger)", this);
            ULogger.LogError("Unsorted error (ULogger)", this);
        }
    }
}