using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONObjectInterface {

    /// <summary>
    /// Generate a JSON with the ScriptableObject information
    /// </summary>
    /// <returns></returns>
    public static string BuildJSON<T>(T obj){
        return JsonUtility.ToJson(obj);
    }

    /// <summary>
    /// Overwrite the information in the ScriptableObject from the JSON representation
    /// </summary>
    /// <param name="json"></param>
    public static void ExtractFromJSON<T>(T obj, string json){
        JsonUtility.FromJsonOverwrite(json, obj);
    }

    /// <summary>
    /// Return an object of type T from a json
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="json"></param>
    /// <returns></returns>
    public static T BuildFromJSON<T>(string json){
        return JsonUtility.FromJson<T>(json);
    }
}
