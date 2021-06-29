using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController
{
    public static MenuController instance = null;

    public MenuController getInstance()
    {
        return instance;
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
