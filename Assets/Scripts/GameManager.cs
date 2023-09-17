using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private InputHandler input;

    public bool gameIsPaused;

    public GameObject pauseMenu;

    private void Start()
    {
        pauseMenu.SetActive(false);
        input = GameObject.Find("Player").GetComponent<InputHandler>();
    }

    private void Update()
    {

    }

    public void PauseGame()
    {
        if (!gameIsPaused)
        {
            gameIsPaused = true;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
            Resume();
    }

    public void Resume()
    {
        gameIsPaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
