using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTime : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private GameObject _FirtsPanel;
    [SerializeField] private GameObject _NormalPanel;

    // Start is called before the first frame update
    void Start()
    {
        _GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if(_GameManager.Day != 0)
        {
            _FirtsPanel.SetActive(false);
            _NormalPanel.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
