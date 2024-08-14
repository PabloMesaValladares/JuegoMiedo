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
    public Slider _brightnessSlider;
    public Slider _contrastSlider;
    public Toggle _bobheadToggle;

    public Volume _settingsProfile;

    private ColorAdjustments colorsAdjustments;

    public RenderPipelineAsset[] qualityLevels;

    void Start()
    {
        _settingsProfile = FindObjectOfType<Volume>();

        if( _settingsProfile != null )
        {
            _settingsProfile.profile.TryGet<ColorAdjustments>(out colorsAdjustments);
        }
    }

    public void AdjustBrightness()
    {
        if(colorsAdjustments.postExposure.value != 0)
        {
            colorsAdjustments.postExposure.value = _brightnessSlider.value;
        }
        else
        {
            colorsAdjustments.postExposure.value = 0.05f;
        }
    }

    public void AdjustContrast()
    {
        if (colorsAdjustments.contrast.value != 0)
        {
            colorsAdjustments.contrast.value = _brightnessSlider.value;
        }
        else
        {
            colorsAdjustments.contrast.value = 0;
        }
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
        //volumes
    }

    public void bobheadEnable()
    {
        if(_bobheadToggle == true)
        {

        }
        else if(_bobheadToggle == false)
        {

        }
    }

    //contraste, graficos, volumen, pantalla completa, bobhead, 
}
