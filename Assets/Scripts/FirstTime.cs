using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTime : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private GameObject _ContinueButton;
    [SerializeField] private GameObject _FakeContinueButton;

    // Start is called before the first frame update
    void Start()
    {
        _GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if(_GameManager.Day != 0)
        {
            _FakeContinueButton.SetActive(false);
            _ContinueButton.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
