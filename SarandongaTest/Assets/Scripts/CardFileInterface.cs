using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CardFileInterface : MonoBehaviour {
    
    public static CardFileInterface instance;
    private const string fileName = "/JSONFiles/Cards.json";

    private void Start() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Reads all the lines on the JSON file
    /// </summary>
    /// <returns></returns>
    public static string[] ReadAllLines() {
        return File.ReadAllLines(Application.dataPath + fileName);
    }

    /// <summary>
    /// Adds the Card information to the end of the file
    /// </summary>
    /// <param name="card"></param>
    public static void AppendLine(Card card) {
        File.AppendAllText(Application.dataPath + fileName, "\r\n" + JsonUtility.ToJson(card));
    }

    /// <summary>
    /// Return a random line from the JSON file
    /// </summary>
    /// <returns></returns>
    public static string RandomLine() {
        string[] cards = ReadAllLines();
        return cards[Random.Range(0, cards.Length)];
    }

    private void Update() {
    /* 
     *SOLO PARA DEBUG - ELIMINAR AL FINAL 
     
        if (Input.GetKeyDown(KeyCode.Space)) {
            GameController.Deal();
        }*/
    }
}