using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private GameObject _Player;
    [SerializeField] private GameObject _PlayerCamera;
    [SerializeField] private bool playerIsHidden;

    public UnityEvent startMinigame;

    // Start is called before the first frame update
    void Awake()
    {
        playerIsHidden = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerHidden()
    {
        if(playerIsHidden == false)
        {
            playerIsHidden = true;
            _Player.SetActive(false);
            _PlayerCamera.SetActive(false);
            startMinigame.Invoke();
        }
        else if(playerIsHidden == true) 
        {
            playerIsHidden = false;
            _Player.SetActive(true);
            _PlayerCamera.SetActive(true);
        }
    }
}
