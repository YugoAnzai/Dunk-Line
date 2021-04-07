using UnityEngine;

public static class Logger
{

    public static void Log(string message)
    {

        if (!UnityEngine.Debug.isDebugBuild)
            return;

        UnityEngine.Debug.Log(message);

    }


}