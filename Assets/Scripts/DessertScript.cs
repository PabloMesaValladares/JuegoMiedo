using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DessertScript : MonoBehaviour
{
    [SerializeField] private GameplayManager _GameplayManager;
    [SerializeField] private GameObject _DessertObject;
    [SerializeField] private List<GameObject> _DessertPositions;

    // Start is called before the first frame update
    public void SpawnTheDessert(int position)
    {
        _DessertObject.transform.position = _DessertPositions[position].transform.position;
        _DessertObject.SetActive(true);
    }

    public void DessertPicked()
    {
        _GameplayManager.GetComponent<GameplayManager>().gluttonyDessertPicked = true;
    }
}
