using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
[Serializable]
public class Card : ScriptableObject {

    public new string name;
    [TextArea]
    public string description;

    public string BuildJSON() {
        return JsonUtility.ToJson(this);
    }

    public void ExtractJSON(string json) {
        JsonUtility.FromJsonOverwrite(json, this);
    }
}
