using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONFileInterface {

    /// <summary>
    /// Reads all the lines on the JSON file
    /// </summary>
    /// <returns></returns>
    public static string[] ReadAllLines(string fileName) {
        return File.ReadAllLines(Application.dataPath + fileName);
    }

    /// <summary>
    /// Adds the Card information to the end of the file
    /// </summary>
    /// <param name="card"></param>
    public static void AppendLine(string text, string fileFolder) {
        File.AppendAllText(Application.dataPath + JSONPaths.path + fileFolder + JSONPaths.fileName, /*"\r\n" + */text);
    }

    /// <summary>
    /// Return a random line from the JSON file
    /// </summary>
    /// <returns></returns>
    public static string RandomLine(string fileName) {
        string[] cards = ReadAllLines(JSONPaths.path + fileName);
        return cards[Random.Range(0, cards.Length)];
    }
}