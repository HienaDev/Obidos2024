using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameScene;
    [SerializeField] private string creditsScene;

    public void StartGame()
    {
        SceneManager.LoadScene(gameScene);
    }
    public void Credits()
    {
        SceneManager.LoadScene(creditsScene);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
