using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("Level One");
    }
}
