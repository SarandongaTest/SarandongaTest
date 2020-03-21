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
        SceneManager.LoadScene("PhoneScene");
    }

    public void CloseApp() {
        Application.Quit();
    }

    public void PlayCard() {
        PlayerHand.instance.PlayCard();
    }
}
