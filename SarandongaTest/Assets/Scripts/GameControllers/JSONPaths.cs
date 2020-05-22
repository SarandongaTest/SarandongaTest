using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JSONPaths {
    public static readonly string fileName = "/Deck.json";
    public static readonly string jsonPath = "/JSONFiles";
    public static readonly string deckPath = "/Deck";
    public static readonly string languagePath = "/lan";
    public static readonly string pastebinPath = "https://pastebin.com/raw/";

    public static string GetDeckPath() {
        return Application.dataPath + jsonPath + deckPath + "/" + LanguageTags.instance.lan;
    }

    public static string GetLanguagePath() {
        return Application.dataPath + jsonPath + languagePath + "/";
    }
}

