using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    #region Singleton
    public static MenuManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    // method to play game on button click
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    // method to quit game
    public void QuitApplication()
    {
        Application.Quit();
    }

}
