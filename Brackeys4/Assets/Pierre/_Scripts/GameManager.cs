using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject winScreem;
    public GameObject loseScreen;
    public GameObject credits;
    private bool paused = false;
    private bool ended = false;
    public int totalFixes = 0;
    private CoffeeShopManager cManager;

    private void Start()
    {
        cManager = FindObjectOfType<CoffeeShopManager>();
        winScreem.GetComponent<Animator>().SetBool("End", false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                Pause();
                
            } else
            {
                Resume();
            }
        }

        if(ended && Input.GetKeyDown(KeyCode.Space))
        {
            ended = false;
            Time.timeScale = 1;
            loseScreen.GetComponent<Fader>().Fade(0);
            //cManager.GetComponent<AudioSource>().Play();
        }
    }

    public void Pause()
    {
        paused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        //pauseMenu.GetComponent<Animation>().Play("PauseAnimation");
    }

    public void Resume()
    {
        paused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        var scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public void End()
    {
        ended = true;
        //cManager.GetComponent<AudioSource>().Pause();
        Time.timeScale = 0.001f;
        loseScreen.SetActive(true);
        loseScreen.GetComponent<Fader>().Fade(1);
    }

    public void Win()
    {
        //Time.timeScale = 0;
        winScreem.GetComponent<Animator>().SetBool("End", true);
        credits.SetActive(true);
    }
}
