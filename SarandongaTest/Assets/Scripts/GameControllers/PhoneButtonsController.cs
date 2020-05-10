using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhoneButtonsController : MonoBehaviour {

    public static PhoneButtonsController instance;
    public GameObject PlayButton;
    public GameObject DecideButton;

    private void Awake() {
        instance = this;
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void LoadPhoneScene() {
        MenuController.LoadDecks();
        //SceneManager.LoadScene("PhoneScene");
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
        PlayButton.GetComponent<Button>().interactable = playing;
        PlayButton.SetActive(playing);
        DecideButton.GetComponent<Button>().interactable = playing;
        DecideButton.SetActive(!playing);
    }

    public void SetPlayNotInteractable() {
        PlayButton.GetComponent<Button>().interactable = false;
    }

    public void SetPlayInteractable() {
        PlayButton.GetComponent<Button>().interactable = true;
    }

    public void SetDecideInteractable() {
        DecideButton.GetComponent<Button>().interactable = true;
    }

}
