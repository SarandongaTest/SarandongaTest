using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardJSONInterface : MonoBehaviour {

    /// <summary>
    /// Generate a JSON with the ScriptableObject information
    /// </summary>
    /// <returns></returns>
    public string BuildJSON<T>(T obj) where T : ScriptableObject{
        return JsonUtility.ToJson(obj);
    }

    /// <summary>
    /// Overwrite the information in the ScriptableObject from the JSON representation
    /// </summary>
    /// <param name="json"></param>
    public void ExtractFromJSON<T>(T obj, string json) where T : ScriptableObject{
        JsonUtility.FromJsonOverwrite(json, obj);
    }

    /// <summary>
    /// Return any ScriptableObject from a json
    /// </summary>
    /// <typeparam name="T">ScriptableObject</typeparam>
    /// <param name="json"></param>
    /// <returns></returns>
    public static T BuildFromJSON<T>(string json) where T : ScriptableObject {
        T obj = ScriptableObject.CreateInstance<T>();
        JsonUtility.FromJsonOverwrite(json, obj);
        return obj;
    }
}
