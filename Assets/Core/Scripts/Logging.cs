using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Logging
{
    public static void LogFormat(string message, params object[] args)
    {
        Debug.LogFormat(message, args);
    }
}