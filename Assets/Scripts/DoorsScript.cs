using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class DoorsScript : MonoBehaviour
{
    [SerializeField] private Animator _Animator;
    [SerializeField] private bool isOpen;
    [SerializeField] private bool blockedState;
    // Start is called before the first frame update
    void Start()
    {
        _Animator.SetInteger("State", 0);
    }
    /*
    // Update is called once per frame
    void Update()
    {

    }
    */
    public void AnimationOpenDoor() 
    {
        if(isOpen == false && blockedState == false)
        {
            _Animator.SetInteger("State", 1);
            isOpen = true;
        }
        else if(isOpen == true && blockedState == false)
        {
            _Animator.SetInteger("State", 2);
            isOpen = false;
        }

    }

    public void AnimationBlockDoor()
    {
        if (isOpen == false && blockedState == false)
        {
            _Animator.SetInteger("State", 0);
            blockedState = true;
        }
        else if (isOpen == true && blockedState == false)
        {
            _Animator.SetInteger("State", 2);
            isOpen = false;
            blockedState = true;
        }
        else if(blockedState == true)
        {
            blockedState = false;
        }
    }
}
