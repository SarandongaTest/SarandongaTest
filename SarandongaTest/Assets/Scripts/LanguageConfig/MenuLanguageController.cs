using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuLanguageController : MonoBehaviour
{
    public static MenuLanguageController instance;

    public Text decksTittle;
    public Text deckCodePlaceholder;
    public Text deckLoadButton;
    public Text gamePlaceholder;
    public Text hostGameButton;
    public Text joinGameButton;
    //public Text joinButton;
    public Text gamesTittle;
    public Text refreshButton;
    //public Text waitingButton;
    //public Text readyButton;

    private void Awake() {
        //LanguageTags settings = new LanguageTags(Application.systemLanguage.ToString());
        LanguageTags settings = new LanguageTags();
    }

    private void Start() {
        instance = this;
        Setup();
    }


    public void Setup() {
        LanguageTags settings = LanguageTags.instance;

        decksTittle.text = settings.decksTittle;
        deckCodePlaceholder.text = settings.deckPlaceholderText;
        deckLoadButton.text = settings.deckLoadButton;
        gamePlaceholder.text = settings.gamePlaceholderText;
        hostGameButton.text = settings.hostGameButton;
        joinGameButton.text = settings.joinGameButton;
        //joinButton.text = settings.joinButton;
        gamesTittle.text = settings.gamesTittle;
        refreshButton.text = settings.refreshButton;
        //waitingButton.text = settings.waitingButton;
        //readyButton.text = settings.readyButton;

    }
}
