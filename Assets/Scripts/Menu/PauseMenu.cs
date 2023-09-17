using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MenuController
{
    private GameManager gameManager;

    private void Start()
    {
        currentMenu = "Start";
        gameManager = GetComponent<GameManager>();
        SwitchBackButton();
    }

    public void ResumeGame()
    {
        gameManager.Resume();
        SwitchBackButton();
    }

    public void SaveGame()
    {
        SwitchMenu(currentMenu, "SaveGame");
        currentMenu = "SaveGame";
        SwitchBackButton(true);
    }

    public void LoadGame()
    {
        SwitchMenu(currentMenu, "LoadGame");
        currentMenu = "LoadGame";
        SwitchBackButton(true);
    }

    public void Settings()
    {
        SwitchMenu(currentMenu, "Settings");
        currentMenu = "Settings";
        SwitchBackButton(true);
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
     
}
