using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closet : MonoBehaviour
{
    [SerializeField] private GameObject _GameplayManager;
    [SerializeField] private GameObject hiddenCamera;
    [SerializeField] private bool playerInside;
    [SerializeField] HiddenMinigame _hiddenMinigame;

    public void HiddenInCloset()
    {
        if (!playerInside && _GameplayManager.GetComponent<GameplayManager>().PlayerCantHiddenConfirmation() == false)
        {
            playerInside = true;
            _GameplayManager.GetComponent<GameplayManager>().PlayerHidden();
            hiddenCamera.SetActive(true);
        }
        else if (playerInside && _hiddenMinigame.startMinigame == false)
        {
            Debug.Log("intento salir");
            playerInside = false;
            _GameplayManager.GetComponent<GameplayManager>().PlayerHidden();
            hiddenCamera.SetActive(false);
        }
    }

    public bool playerIsInside()
    {
        return playerInside;
    }
}

