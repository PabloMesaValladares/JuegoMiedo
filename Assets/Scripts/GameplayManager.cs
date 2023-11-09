using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private GameObject _Player;
    [SerializeField] private GameObject _PlayerCamera;
    [SerializeField] private bool playerIsHidden;

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
        }
        else if(playerIsHidden == true) 
        {
            playerIsHidden = false;
            _Player.SetActive(true);
            _PlayerCamera.SetActive(true);
        }
    }
}
