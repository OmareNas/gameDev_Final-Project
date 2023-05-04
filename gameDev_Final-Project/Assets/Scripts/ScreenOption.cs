using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenOption : MonoBehaviour
{
   public Dropdown resolutionDropdown;
   public Toggle isFullScreen;

   Resolution[] resolutions;

   void Start() 
   {
        resolutions = Screen.resolutions;
        isFullScreen.isOn = Screen.fullScreen;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string resolutionStr = resolutions[i].width.ToString() + "x" + resolutions[i].height.ToString();
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolutionStr));

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                resolutionDropdown.value = i;
            } 
        }
    
   }

   public void setResolution()
   {
    int width = resolutions[resolutionDropdown.value].width;
    int height = resolutions[resolutionDropdown.value].height;
    Screen.SetResolution(width,height, isFullScreen.isOn);
   }
}
