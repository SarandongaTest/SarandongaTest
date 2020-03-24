using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONFileInterface : MonoBehaviour {
    
    public static JSONFileInterface instance;

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
    public static string[] ReadAllLines(string fileName) {
        /*DirectoryInfo info = new DirectoryInfo(Application.dataPath + JSONPaths.path);
        var infoDir = info.GetDirectories();
        foreach (string file in Directory.GetDirectories(Application.dataPath + JSONPaths.path)) {
            Debug.Log(file);
        }*/
        return File.ReadAllLines(Application.dataPath + fileName);
    }

    /// <summary>
    /// Adds the Card information to the end of the file
    /// </summary>
    /// <param name="card"></param>
    /*public static void AppendLine(Card card) {
        File.AppendAllText(Application.dataPath + fileName, "\r\n" + JsonUtility.ToJson(card));
    }*/

    /// <summary>
    /// Return a random line from the JSON file
    /// </summary>
    /// <returns></returns>
    public static string RandomLine(string fileName) {
        string[] cards = ReadAllLines(fileName);
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