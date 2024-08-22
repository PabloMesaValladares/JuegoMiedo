using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameDataController : MonoBehaviour
{
    public static GameDataController instance;

    public GameObject gameManager;
    public string archivoDeGuardado;
    public GameData gameData = new GameData();

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
            archivoDeGuardado = Application.persistentDataPath + "/gameData.json";
            gameManager = GameObject.Find("GameManager");
            LoadData();
        } else if (instance != this) {
            Destroy(gameObject);
        }
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
            ApplyLoadedData();
            Debug.Log("Cargado");
        } else {
            Debug.LogError("No se encontró el archivo de guardado");
        }
    }

    public void SaveData() {
        gameData.day = gameManager.GetComponent<GameManager>().Day;

        string cadenaJSON = JsonUtility.ToJson(gameData);
        File.WriteAllText(archivoDeGuardado, cadenaJSON);

        Debug.Log("Guardado");
    }

    public void DeleteData() {
        if (File.Exists(archivoDeGuardado)) {
            File.Delete(archivoDeGuardado);
            Debug.Log("Eliminado");
        } else {
            Debug.LogError("No se encontró el archivo de guardado");
        }
    }

    public void ResetData() {
        gameManager.GetComponent<GameManager>().Day = 0;
        SaveData();
    }

    private void ApplyLoadedData() {
        var globalSettings = FindObjectOfType<GlobalSettings>();
        if (globalSettings != null) {
            globalSettings._brightnessSlider.value = gameData.brightness;
            globalSettings._contrastSlider.value = gameData.contrast;
            globalSettings._bobheadToggle.isOn = gameData.bobhead;

            globalSettings.colorsAdjustments.postExposure.value = gameData.brightness;
            globalSettings.colorsAdjustments.contrast.value = gameData.contrast;

            globalSettings.ChangeQuality(gameData.quality);
            globalSettings.ChangeScreenMode(gameData.screenMode);
        }
    }
}