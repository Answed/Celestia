using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MenuController
{
    private void Start()
    {
        currentMenu = "Start";
    }

    public void ResumeGame()
    {

    }

    public void SaveGame()
    {
        SwitchMenu(currentMenu, "SaveGame");
        currentMenu = "SaveGame";
    }

    public void LoadGame()
    {
        SwitchMenu(currentMenu, "LoadGame");
        currentMenu = "LoadGame";
    }

    public void Settings()
    {
        SwitchMenu(currentMenu, "Settings");
        currentMenu = "Settings";
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
     
}
