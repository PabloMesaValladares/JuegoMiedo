using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MenusController : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("Gameplay");
        //SoundManager.Instance.StopSounds();
        //SoundManager.Instance.PlayeSound("MainTheme", true);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
        //SoundManager.Instance.StopSounds();
        //SoundManager.Instance.PlayeSound("MainTheme", true);
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
        //SoundManager.Instance.StopSounds();
        //SoundManager.Instance.PlayeSound("MainTheme", true);
    }

    public void Exit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
