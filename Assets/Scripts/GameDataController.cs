using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameDataController : MonoBehaviour
{
    public GameObject gameManager;
    public string archivoDeGuardado;
    private GameData gameData = new GameData();
    private  void Awake() {
        DontDestroyOnLoad(gameObject);
        archivoDeGuardado = Application.dataPath + "/gameData.json";
        gameManager = GameObject.Find("GameManager");
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.X)) {
            SaveData();
        }
    }

    public void LoadData() {
        if (File.Exists(archivoDeGuardado)) {
            string json = File.ReadAllText(archivoDeGuardado);
            gameData = JsonUtility.FromJson<GameData>(json);
            gameManager.GetComponent<GameManager>().Day = gameData.day;
            Debug.Log("Cargado");
        } else {
            Debug.LogError("No se encontro el archivo de guardado");
        }
    }

    public void SaveData() {
        GameData newData = new GameData()
        {
            day = gameManager.GetComponent<GameManager>().Day
        };
        string cadenaJSON = JsonUtility.ToJson(newData);
        File.WriteAllText(archivoDeGuardado, cadenaJSON);

        Debug.Log("Guardado");
    }

    public void DeleteData() {
        if (File.Exists(archivoDeGuardado)) {
            File.Delete(archivoDeGuardado);
            Debug.Log("Eliminado");
        } else {
            Debug.LogError("No se encontro el archivo de guardado");
        }
    }

    public void ResetData() {
        gameManager.GetComponent<GameManager>().Day = 0;
        SaveData();
    }
}
