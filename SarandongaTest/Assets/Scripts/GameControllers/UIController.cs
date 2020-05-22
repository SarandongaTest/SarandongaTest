using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Text tittleText;
    public static UIController instance;
    public GameObject PlayButton;
    public GameObject DecideButton;
    public Text scoreText;

    private void Awake() {
        instance = this;
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void CloseApp() {
        Application.Quit();
    }

    public void PlayCard() {
        PlayerHand.instance.PlayCard();
    }

    public void SelectWinner() {
        PlayerHand.instance.SelectWinnerCard();
    }

    /// <summary>
    /// Sets the Tittle and the Buttons to the correct state
    /// </summary>
    /// <param name="playing"></param>
    public void SetPlayButtons(bool playing) {
        SetPlayButtonAndTittle(playing);
        PlayButton.SetActive(playing);
        DecideButton.GetComponent<Button>().interactable = playing;
        DecideButton.SetActive(!playing);
    }

    public void SetPlayButtonAndTittle(bool state) {
        tittleText.text = state ? LanguageTags.instance.playACardTittle : LanguageTags.instance.waitingForPlayersTittle;
        PlayButton.GetComponent<Button>().interactable = state;
    }

    public void SetDecideInteractable() {
        tittleText.text = LanguageTags.instance.selectAWinnerTittle;
        DecideButton.GetComponent<Button>().interactable = true;
    }

    public void SetSelectingTittle() {
        tittleText.text = LanguageTags.instance.waitingForSelectTittle;
    }

    public void UpdateScoreText(int score) {
        scoreText.text = LanguageTags.instance.pointsTittle + score;
    }

    /*public static void ParseDeck() {
        DeckParser deckParser = JSONObjectInterface.BuildFromJSON<DeckParser>(JSONFileInterface.RandomLine("Base/original.json"));
        deckParser.StoreDeck();
    }*/
}
