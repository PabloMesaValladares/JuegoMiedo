using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNight : MonoBehaviour
{
    [SerializeField] private GameObject _GameManager;
    [SerializeField] private GameObject _GameplayManager;
    [SerializeField] private GameObject _Player;
    [SerializeField] private GameObject _PlayerSpawnPointNight;
    [SerializeField] private GameObject _PlayerSpawnPointDay;

    [SerializeField] private int maxNight;
    // Start is called before the first frame update

    public void StartNextNight()
    {
        _GameplayManager.SetActive(true);
        _GameplayManager.GetComponent<GameplayManager>().StartGame();
        _Player.transform.position = _PlayerSpawnPointNight.transform.position;
        gameObject.GetComponent<StartNight>().enabled = false;
        //Que pille la info del GameManager y todo esto (creo que no hara falta al final)
    }

    public void NightIsEnded()
    {
        _GameplayManager.GetComponent<GameplayManager>().FinishGame();
        _GameplayManager.SetActive(false);
        _Player.transform.position = _PlayerSpawnPointDay.transform.position;
        gameObject.GetComponent<StartNight>().enabled = true;
        if(_GameManager.GetComponent<GameManager>().Day < _GameManager.GetComponent<GameManager>().maxDay)
        {
            _GameManager.GetComponent<GameManager>().Day += 1;
        }
    }

    public IEnumerator NightIsEnding()
    {
        yield return null;
        //aqui hay que hacer la animacion final de haber pasado de dia
    }
}
