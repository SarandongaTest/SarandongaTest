using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CardFileInterface : MonoBehaviour {
    
    public GameObject cardPrefab;
    public static CardFileInterface instance;

    private void Start() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    private const string fileName = "/JSONFiles/Cards.json";

    public string[] ReadAllLines() {
        return File.ReadAllLines(Application.dataPath + fileName);
    }

    public void AppendLine(Card card) {
        File.AppendAllText(Application.dataPath + fileName, "\r\n" + JsonUtility.ToJson(card));
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Deal();
        }
    }

    //todo pasar esto a un gamecontroller
    public void Deal() {
        string[] cards = ReadAllLines();
        PlayerHand.instance.AddCard(
                        CardDisplay.InstanciateCard(Card.CardFromJSON(cards[Random.Range(0, cards.Length)]),
                        cardPrefab,
                        PlayerHand.instance.gameObject));
    }

}