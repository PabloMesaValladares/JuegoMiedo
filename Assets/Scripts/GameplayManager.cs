using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private GameObject _Player;
    [SerializeField] private GameObject _PlayerCamera;
    [SerializeField] private bool playerIsHidden;
    [SerializeField] private bool playerCantHidden;
    [SerializeField] private bool lanternActivated;
    [SerializeField] private GameObject _GameOverUI;
    [SerializeField] private GameObject _OthersUI;

    public UnityEvent startMinigame;

    [SerializeField] private List<GameObject> _prideSpawns;

    //Enemies
    [SerializeField] private GameObject Pride;
    [SerializeField] private GameObject Gluttony;
    [SerializeField] private GameObject Greed;

    //Enemies Things
    [SerializeField] private int prideRandomNumber;
    [SerializeField] private int prideRandomNumberMax;

    // Start is called before the first frame update
    void Awake()
    {
        //Time.timeScale = 1;
        playerIsHidden = false;
        playerCantHidden = false;
        lanternActivated = false;
        prideRandomNumber = 0;
}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerHidden()
    {
        if(playerIsHidden == false && playerCantHidden == false)
        {
            playerIsHidden = true;
            _Player.SetActive(false);
            _PlayerCamera.SetActive(false);
        }
        else if(playerIsHidden == true) 
        {
            playerIsHidden = false;
            _Player.SetActive(true);
            _PlayerCamera.SetActive(true);
        }
    }

    public void LanternIsActivatted(bool l)
    {
        lanternActivated = l;
    }

    public bool LanternActivated()
    {
        return lanternActivated;
    }

    public void PlayerCantHiddenBecauseGreedIsHere(bool c)
    {
        playerCantHidden = c;
    }

    public bool PlayerCantHiddenConfirmation()
    {
        return playerCantHidden;
    }

    public void StartHiddenMinigame()
    {
        startMinigame.Invoke();
    }

    public void GameOver()
    {
        _Player.SetActive(false);
        _GameOverUI.SetActive(true);
        _OthersUI.SetActive(false);
        Cursor.lockState = CursorLockMode.None;

        //Time.timeScale = 0;
        //demas cositas
    }

    public void PrideIsComing()
    {
        int i = 0;
        prideRandomNumber = UnityEngine.Random.Range(0, prideRandomNumberMax);

        Pride.GetComponent<Pride>().Reset();
        Pride.gameObject.transform.position = _prideSpawns[i].gameObject.transform.position;
        Pride.SetActive(true);
    }


    public void PrideIsOut()
    {
        int i = 0;
        prideRandomNumber = UnityEngine.Random.Range(0, prideRandomNumberMax);

        //Pride.gameObject.transform.position = _prideSpawns[i].gameObject.transform.position;
        Pride.GetComponent<Pride>().OutOfHere(_prideSpawns[i].gameObject.transform.position);
    }
}
