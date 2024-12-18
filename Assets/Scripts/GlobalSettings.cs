using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GlobalSettings : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;
    private GameDataController gameDataController;

    public Slider _brightnessSlider;
    public Slider _contrastSlider;

    public Slider _musicSlider;
    public Slider _sfxSlider;
    public Toggle _bobheadToggle;

    public Volume _settingsProfile;

    [SerializeField] public ColorAdjustments colorsAdjustments;

    public RenderPipelineAsset[] qualityLevels;


    void Awake()
    {
        gameDataController = FindObjectOfType<GameDataController>();
        _GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        _settingsProfile = FindObjectOfType<Volume>();

        if (_settingsProfile != null)
        {
            _settingsProfile.profile.TryGet<ColorAdjustments>(out colorsAdjustments);
        }
    }

    void Start()
    {
        // Load settings
        LoadSettings();
        gameObject.SetActive(false);
    }

    public void AdjustBrightness()
    {
        colorsAdjustments.postExposure.value = _brightnessSlider.value;
        gameDataController.gameData.brightness = _brightnessSlider.value;
        gameDataController.SaveData();
    }

    public void AdjustContrast()
    {
        colorsAdjustments.contrast.value = _contrastSlider.value;
        gameDataController.gameData.contrast = _contrastSlider.value;
        gameDataController.SaveData();
    }

    public void ChangeQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
        QualitySettings.renderPipeline = qualityLevels[quality];
        gameDataController.gameData.quality = quality;
        gameDataController.SaveData();
    }

    public void ChangeScreenMode(int screen)
    {
        if (screen == 0) Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        else if (screen == 1) Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        else if (screen == 2) Screen.fullScreenMode = FullScreenMode.Windowed;
        gameDataController.gameData.screenMode = screen;
        gameDataController.SaveData();
    }

    public void ToggleMusic()
    {
        AudioManager.instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        AudioManager.instance.ToggleSFX();
    }

    public void MusicVolume()
    {
        AudioManager.instance.SetMusicVolume(_musicSlider.value);
        gameDataController.gameData.musicVolume = _musicSlider.value;
        gameDataController.SaveData();
    }

    public void SFXVolume()
    {
        AudioManager.instance.SetSFXVolume(_sfxSlider.value);
        gameDataController.gameData.sfxVolume = _sfxSlider.value;
        gameDataController.SaveData();
    }

    public void bobheadEnable()
    {
        if (_bobheadToggle.isOn)
        {
            _GameManager.Bobhead = true;
        }
        else
        {
            _GameManager.Bobhead = false;
        }
        gameDataController.gameData.bobhead = _bobheadToggle.isOn;
        gameDataController.SaveData();
    }

    public void LoadSettings()
    {
        _brightnessSlider.value = gameDataController.gameData.brightness;
        _contrastSlider.value = gameDataController.gameData.contrast;
        _bobheadToggle.isOn = gameDataController.gameData.bobhead;
        _musicSlider.value = gameDataController.gameData.musicVolume;
        _sfxSlider.value = gameDataController.gameData.sfxVolume;

        colorsAdjustments.postExposure.value = gameDataController.gameData.brightness;
        colorsAdjustments.contrast.value = gameDataController.gameData.contrast;

        ChangeQuality(gameDataController.gameData.quality);
        ChangeScreenMode(gameDataController.gameData.screenMode);
    }
}