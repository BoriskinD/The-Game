using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OptionsMenu : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    private List<string> parsedResolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        ParseResolution();
        resolutionDropdown.AddOptions(parsedResolutions);
    }

    private void ParseResolution()
    {
        for (int i = 0; i < resolutions.Length; i++)
        {
            string currentRes = $"{resolutions[i].width} x {resolutions[i].height}";
            parsedResolutions.Add(currentRes);
        }
    }

    public void SetGameQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    { 
        
    }
}
