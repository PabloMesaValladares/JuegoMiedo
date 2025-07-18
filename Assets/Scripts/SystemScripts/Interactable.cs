using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject buttonImage;
    [SerializeField] private bool inRange;
    [SerializeField] public UnityEvent hasInteracted;

    void Start()
    {
        inRange = false;
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
        if (other.gameObject.tag == "Hand")
        {
            inRange = false;
            buttonImage.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(inRange && Input.GetKeyDown(KeyCode.E))
        {
            hasInteracted.Invoke();
        }
    }
}
