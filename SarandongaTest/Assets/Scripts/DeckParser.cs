using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class DeckParser {
    public List<CardBlack > blackCards = new List<CardBlack >();
    public List<string> whiteCards = new List<string>();
    public string[] order;

    public void StoreDeck() {
        Deck deck = new Deck {
            blackCards = new List<CardBlack >(blackCards)
        };
        
        foreach (string cardInfo in whiteCards) {
            deck.whiteCards.Add(new CardWhite(cardInfo));
        }
        
        Directory.CreateDirectory(Application.dataPath + JSONPaths.jsonPath + order[0]);
        JSONFileInterface.AppendLine(JSONObjectInterface.BuildJSON(deck), order[0] + "/");
    }
}
