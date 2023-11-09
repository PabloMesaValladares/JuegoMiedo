using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closet : MonoBehaviour
{
    [SerializeField] private GameObject buttonImage;
    [SerializeField] private bool inRange;
    [SerializeField] private GameObject _GameplayManager;
    [SerializeField] private GameObject hiddenCamera;
    [SerializeField] private bool playerInside;

    /*
    public void Interact()
    {
        Debug.Log(Random.Range(50, 100));
    }
    */
    // Start is called before the first frame update
    void Start()
    {
        inRange = false;
        playerInside = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hand")
        {
            inRange = true;
            buttonImage.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Hand")
        {
            inRange = false; 
            buttonImage.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E)) 
        {
            if (!playerInside) 
            {
                playerInside = true;
                _GameplayManager.GetComponent<GameplayManager>().PlayerHidden();
                hiddenCamera.SetActive(true);
            }
            else if(playerInside)
            {
                playerInside = false;
                _GameplayManager.GetComponent<GameplayManager>().PlayerHidden();
                hiddenCamera.SetActive(false);
            }

            //Debug.Log(Random.Range(50, 100));
        }
    }
}
