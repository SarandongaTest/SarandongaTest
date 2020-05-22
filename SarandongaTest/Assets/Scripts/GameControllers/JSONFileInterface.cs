using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONFileInterface {
    /// <summary>
    /// Reads all the lines on the JSON file
    /// </summary>
    /// <returns></returns>
    public static string[] ReadAllLines(string path) {
        return File.ReadAllLines(path);
    }

    /// <summary>
    /// Adds the Card information to the end of the file
    /// </summary>
    /// <param name="card"></param>
    public static void AppendLine(string text, string path) {
        File.AppendAllText(path, text);
    }

    /// <summary>
    /// Return a random line from the JSON file
    /// </summary>
    /// <returns></returns>
    public static string RandomLine(string path) {
        string[] cards = ReadAllLines(path);
        return cards[Random.Range(0, cards.Length)];
    }

    public static string ReadLine(string path, int line) {
        return ReadAllLines(path)[line];
    }
}
