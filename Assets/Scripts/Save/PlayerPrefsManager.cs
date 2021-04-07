using System.Collections.Generic;
using UnityEngine;

public enum SaveKey {

    HighScore = 0,
    LastScore = 1,

}

public class PlayerPrefsManager {

    private enum ValueType {
        INT,
        FLOAT,
        BOOL,
        STRING
    }

    private static Dictionary<SaveKey, string> keyNames;
    private static Dictionary<SaveKey, ValueType> keyTypes;

    [RuntimeInitializeOnLoadMethod]
    private static void SetupKeyNames() {
        
        keyNames = new Dictionary<SaveKey, string>();
        keyTypes = new Dictionary<SaveKey, ValueType>();

        // Setup names and types
        AddToDicts(SaveKey.HighScore, ValueType.INT);
        AddToDicts(SaveKey.LastScore, ValueType.INT);

    }

    private static void AddToDicts(SaveKey key, ValueType type) {
        AddToDicts(key, type, key.ToString());
    }

    private static void AddToDicts(SaveKey key, ValueType type, string name) {
        keyTypes.Add(key, type);
        keyNames.Add(key, name);
    }

    public static string GetPrefSaveName(SaveKey key) {
        return keyNames[key];
    }

    public static bool HasKey(SaveKey key) {
        return PlayerPrefs.HasKey(keyNames[key]);
    }

    public static void DeleteKey(SaveKey key) {
        PlayerPrefs.DeleteKey(keyNames[key]);
    }

    #region INT
    public static int GetInt(SaveKey key) {
        if (keyTypes[key] != ValueType.INT) {
            throw new PlayerPrefsException(keyNames[key] + " Does not store INT value.");
        }
        return PlayerPrefs.GetInt(keyNames[key]);
    }

    public static void SetInt(SaveKey key, int value) {
        if (keyTypes[key] != ValueType.INT) {
            throw new PlayerPrefsException(keyNames[key] + " Does not store INT value.");
        }
        PlayerPrefs.SetInt(keyNames[key], value);
    }
    #endregion

    #region BOOL
    public static bool GetBool(SaveKey key) {
        if (keyTypes[key] != ValueType.BOOL) {
            throw new PlayerPrefsException(keyNames[key] + " Does not store Bool value.");
        }
        return PlayerPrefs.GetInt(keyNames[key]) == 1 ? true : false;
    }

    public static void SetBool(SaveKey key, bool value) {
        if (keyTypes[key] != ValueType.BOOL) {
            throw new PlayerPrefsException(keyNames[key] + " Does not store Bool value.");
        }
        int integer = value ? 1 : 0;
        PlayerPrefs.SetInt(keyNames[key], integer);
    }
    #endregion

    #region FLOAT
    public static float GetFloat(SaveKey key) {
        if (keyTypes[key] != ValueType.FLOAT) {
            throw new PlayerPrefsException(keyNames[key] + " Does not store FLOAT value.");
        }
        return PlayerPrefs.GetFloat(keyNames[key]);
    }

    public static void SetFloat(SaveKey key, float value) {
        if (keyTypes[key] != ValueType.FLOAT) {
            throw new PlayerPrefsException(keyNames[key] + " Does not store FLOAT value.");
        }
        PlayerPrefs.SetFloat(keyNames[key], value);
    }
    #endregion

    #region STRING
    public static string GetString(SaveKey key) {
        if (keyTypes[key] != ValueType.STRING) {
            throw new PlayerPrefsException(keyNames[key] + " Does not store STRING value.");
        }
        return PlayerPrefs.GetString(keyNames[key]);
    }
    public static void SetString(SaveKey key, string value) {
        if (keyTypes[key] != ValueType.STRING) {
            throw new PlayerPrefsException(keyNames[key] + " Does not store STRING value.");
        }
        PlayerPrefs.SetString(keyNames[key], value);
    }
    #endregion

    #region RESET
    public static void ResetSave() {
        foreach(SaveKey key in keyNames.Keys) {
            DeleteKey(key);
        }
    }
    #endregion
}

[System.Serializable]
public class PlayerPrefTypeException : System.Exception
{
    public PlayerPrefTypeException() { }
    public PlayerPrefTypeException(string message) : base(message) { }
    public PlayerPrefTypeException(string message, System.Exception inner) : base(message, inner) { }
    protected PlayerPrefTypeException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}