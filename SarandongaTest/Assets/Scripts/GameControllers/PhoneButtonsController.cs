using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhoneButtonsController : MonoBehaviour
{
    public void LoadMainMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void LoadPhoneScene() {
        MenuController.instance.LoadDecks();
        SceneManager.LoadScene("PhoneScene");
    }

    public void CloseApp() {
        Application.Quit();
    }

    public void PlayCard() {
        PlayerHand.instance.PlayCard();
    }

    public void ParseDeck() {
        DeckParser deckParser = JSONObjectInterface.BuildFromJSON<DeckParser>(JSONFileInterface.RandomLine("Base/original.json"));
        deckParser.StoreDeck();
    }
}
