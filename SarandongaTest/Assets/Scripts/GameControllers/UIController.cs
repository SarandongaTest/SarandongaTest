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

    public void LoadDecks() {
        MenuController.LoadDecks();
    }

    public void CloseApp() {
        Application.Quit();
    }

    public void PlayCard() {
        PlayerHand.instance.PlayCard();
    }

    public void DecideCard() {
        PlayerHand.instance.SelectWinnerCard();
    }

    public static void ParseDeck() {
        DeckParser deckParser = JSONObjectInterface.BuildFromJSON<DeckParser>(JSONFileInterface.RandomLine("Base/original.json"));
        deckParser.StoreDeck();
    }

    public void SetPlayButtons(bool playing) {
        tittleText.text = playing ? LanguageTags.playACardTittle : LanguageTags.waitingForPlayersTittle;
        PlayButton.GetComponent<Button>().interactable = playing;
        PlayButton.SetActive(playing);
        DecideButton.GetComponent<Button>().interactable = playing;
        DecideButton.SetActive(!playing);
    }

    public void SetPlayNotInteractable() {
        tittleText.text = LanguageTags.waitingForPlayersTittle;
        PlayButton.GetComponent<Button>().interactable = false;
    }

    public void SetPlayInteractable() {
        tittleText.text = LanguageTags.playACardTittle;
        PlayButton.GetComponent<Button>().interactable = true;
    }

    public void SetDecideInteractable() {
        tittleText.text = LanguageTags.selectAWinnerTittle;
        DecideButton.GetComponent<Button>().interactable = true;
    }

    public void UpdateScoreText(int score) {
        scoreText.text = LanguageTags.pointsTittle + score;
    }

}
