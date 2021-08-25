using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    private AudioSource gameMusic;

    // Update is called once per frame
    private void Start()
    {
        pauseMenuUI.SetActive(false);
        gameMusic = GameObject.Find("Music").GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Pause"))
        {
            if (GameIsPaused)
            {
                Resume();

            }
            else

            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
        
        gameMusic.Play();

    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPaused = true;

        gameMusic.Pause();

    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
