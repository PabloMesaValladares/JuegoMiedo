using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFieldView : MonoBehaviour
{
    [SerializeField] public FieldViewEnemies sensor;

    void Start()
    {
        sensor = GetComponentInChildren<FieldViewEnemies>();
    }
}
