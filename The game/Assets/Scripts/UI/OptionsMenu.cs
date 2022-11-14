using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public Slider volumeSlider;

    private Resolution[] resolutions;
    private List<string> parsedResolutions = new();
    private int currentResolutionIndex = 0;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        ParseResolution();

        resolutionDropdown.AddOptions(parsedResolutions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void ParseResolution()
    {
        for (int i = 0; i < resolutions.Length; i++)
        {
            string currentRes = $"{resolutions[i].width} x {resolutions[i].height}";
            parsedResolutions.Add(currentRes);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
    }

    public void SetGameQuality(int qualityIndex) => QualitySettings.SetQualityLevel(qualityIndex);

    public void SetFullScreen(bool isFullScreen) => Screen.fullScreen = isFullScreen; 

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume) => Managers.Audio.MusicVolume = volume;
}
