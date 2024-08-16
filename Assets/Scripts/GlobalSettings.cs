using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class GlobalSettings : MonoBehaviour
{

    [SerializeField] private GameManager _GameManager;

    public Slider _brightnessSlider;
    public Slider _contrastSlider;
    public Toggle _bobheadToggle;

    public Volume _settingsProfile;

    [SerializeField] private ColorAdjustments colorsAdjustments;

    public RenderPipelineAsset[] qualityLevels;

    void Awake()
    {
        _GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        _settingsProfile = FindObjectOfType<Volume>();

        if( _settingsProfile != null )
        {
            _settingsProfile.profile.TryGet<ColorAdjustments>(out colorsAdjustments);
        }
    }

    public void AdjustBrightness()
    {
        colorsAdjustments.postExposure.value = _brightnessSlider.value;
    }

    public void AdjustContrast()
    {
        colorsAdjustments.contrast.value = _contrastSlider.value;
    }

    public void ChangeQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
        QualitySettings.renderPipeline = qualityLevels[quality];
    }

    public void ChangeScreenMode(int screen)
    {
        if(screen == 0) Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        else if(screen == 1)Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        else if(screen == 2)Screen.fullScreenMode = FullScreenMode.Windowed;
    }

    public void ChangeVolumes()
    {
        //musica, sonidos
    }

    public void bobheadEnable()
    {
        if(_bobheadToggle == true)
        {
            _GameManager.Bobhead = true;
        }
        else if(_bobheadToggle == false)
        {
            _GameManager.Bobhead = false;
        }
    }
}
