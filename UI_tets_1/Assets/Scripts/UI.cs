using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
                Pause();
            else if (Time.timeScale == 0)
                Resume();
        }
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void MainMenuStart()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitTheGame()
    {
        Application.Quit();
    }
}
