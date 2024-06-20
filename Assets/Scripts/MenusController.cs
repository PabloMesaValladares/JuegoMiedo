using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MenusController : MonoBehaviour
{
    public void ReturndedMenu()
    {
        SceneManager.LoadScene("Menu");
        //SoundManager.Instance.StopSounds();
        //SoundManager.Instance.PlayeSound("MainTheme", true);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
        //SoundManager.Instance.StopSounds();
        //SoundManager.Instance.PlayeSound("MainTheme", true);
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelector");
        //SoundManager.Instance.StopSounds();
        //SoundManager.Instance.PlayeSound("MainTheme", true);
    }
    public void Controls()
    {
        SceneManager.LoadScene("Settings");
        //SoundManager.Instance.StopSounds();
        //SoundManager.Instance.PlayeSound("MainTheme", true);
    }

    public void Instructions()
    {
        SceneManager.LoadScene("HandBook");
        //SoundManager.Instance.StopSounds();
        //SoundManager.Instance.PlayeSound("MainTheme", true);
    }
    public void Instructions2()
    {
        SceneManager.LoadScene("HandBook1");
        //SoundManager.Instance.StopSounds();
        //SoundManager.Instance.PlayeSound("MainTheme", true);
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
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
