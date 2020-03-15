using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject {

    public new string name;
    [TextArea]
    public string description;

    /// <summary>
    /// Generate a JSON with the Card information
    /// </summary>
    /// <returns></returns>
    public string BuildJSON() {
        return JsonUtility.ToJson(this);
    }

    /// <summary>
    /// Overwrite the information in the Card from the JSON representation
    /// </summary>
    /// <param name="json"></param>
    public void ExtractFromJSON(string json) {
        JsonUtility.FromJsonOverwrite(json, this);
    }

    /// <summary>
    /// Create a Card from a JSON representation
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public static Card CardFromJSON(string json) {
        Card card = ScriptableObject.CreateInstance<Card>();
        JsonUtility.FromJsonOverwrite(json, card);
        return card;
    }
}
