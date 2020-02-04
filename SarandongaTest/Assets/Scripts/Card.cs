using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject {

    public new string name;
    [TextArea]
    public string description;

    public Card(string name, string description) {
        this.name = name;
        this.description = description;
    }

    public string BuildJSON() {
        return JsonUtility.ToJson(this);
    }

    public void ExtractFromJSON(string json) {
        JsonUtility.FromJsonOverwrite(json, this);
    }

    public static Card ExtractAndBuildFromJSON(string json) {
        Card card = ScriptableObject.CreateInstance<Card>();
        JsonUtility.FromJsonOverwrite(json, card);
        return card;
    }
}
