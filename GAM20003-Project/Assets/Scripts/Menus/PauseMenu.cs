using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseFirstButton;
    public string menuScene;
    public GameObject pauseMenu;
    public bool isPaused;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)|| Input.GetKeyDown("joystick button 7"))
        {
            if(isPaused)
            {
                resumeGame();
            }
            else
            {
                isPaused = true;
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
                EventSystem.current.SetSelectedGameObject(pauseFirstButton);



            }
        }
    }
    public void resumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
public void mainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuScene);
    }

    public void Lobby()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Lobby");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
        
}


