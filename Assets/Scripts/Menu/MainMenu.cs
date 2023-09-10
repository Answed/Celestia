using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MenuController
{
    private string currentMenu;

    public void Settings()
    {
        SwitchMenu("Start", "Settings");
        currentMenu = "Settings";
    }
    public void Credits()
    {
        SwitchMenu("Start", "Credits");
        currentMenu = "Credits";
    }
    public void Resume()
    {
        DeactivateMenu("Start");
    }
    public void Back()
    {
        SwitchMenu(currentMenu, "Start");
        currentMenu = "Start";
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
