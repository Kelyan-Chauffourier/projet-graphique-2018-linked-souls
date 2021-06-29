using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingView : MonoBehaviour {

    public Slider volumeSlider;
    public GameObject canvasMenuPrincipal;
    public GameObject canvasSettingMenu;

    public void OnValueChanged()
    {
        AudioListener.volume = volumeSlider.value;
    }

    public void OnBackButtonPressed()
    {
        canvasMenuPrincipal.SetActive(true);
        canvasSettingMenu.SetActive(false);
    }
}
