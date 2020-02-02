using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CardFileInterface : MonoBehaviour {
    
    public GameObject cardPrefab;

    private const string fileName = "/JSONFiles/Cards.json";

    void Start() {

        /*foreach (string json in ReadAllLines()) {
            GameObject instance = CardDisplay.InstanciateCard(Card.ExtractAndBuildFromJSON(json), cardPrefab, playerHand);
            playerHand.GetComponent<PlayerHand>().AddCard(instance);
        }*/
    }

    public string[] ReadAllLines() {
        return File.ReadAllLines(Application.dataPath + fileName);
    }

    public void AppendLine(Card card) {
        File.AppendAllText(Application.dataPath + fileName, "\r\n" + JsonUtility.ToJson(card));
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            PlayerHand.instance.AddCard(
                CardDisplay.InstanciateCard(Card.ExtractAndBuildFromJSON(ReadAllLines()[0]),
                cardPrefab,
                PlayerHand.instance.gameObject));
        }
    }
}
