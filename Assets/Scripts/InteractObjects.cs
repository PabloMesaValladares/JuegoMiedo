using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface InteractableObj
{
    public void Interact();
}

public class InteractObjects : MonoBehaviour
{
    public LayerMask seeObjects;
    public Transform interactorInfo;
    public float interactRange;
    public float popUpRange;

    public Canvas interactCanvas;
    public GameObject popUpButtonE;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            Ray r = new Ray(interactorInfo.position, interactorInfo.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, interactRange, seeObjects)) 
            {
                if(hitInfo.collider.gameObject.TryGetComponent(out InteractableObj interactObj)) 
                {
                    interactObj.Interact();
                }
            }
        }

        Ray pop = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Ray pop = new Ray(interactorInfo.position, interactorInfo.forward);
        if (Physics.Raycast(pop, out RaycastHit hitedObject, popUpRange))
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;
            if (hitedObject.collider.gameObject.TryGetComponent(out InteractableObj interactObj))
            {
                popUpButtonE.SetActive(true);
            }
            else if(!hitedObject.collider.gameObject.TryGetComponent(out InteractableObj interactObj2))
            {
                popUpButtonE.SetActive(false);
            }

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * popUpRange, Color.red);
        }
    }
}
