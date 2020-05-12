using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public Text Score;
    public Button btnPause;
    public Canvas PauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int points)
    {
        Score.text = string.Concat(points.ToString(), " points");
    }

    public void Pause()
    {
        Time.timeScale = 0;
        btnPause.gameObject.SetActive(false);
        PauseMenu.gameObject.SetActive(true);

    }

    public void UnPause()
    {
        Time.timeScale = 1;
        btnPause.gameObject.SetActive(true);
        PauseMenu.gameObject.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("First");
    }

    public void Exit()
    {
        SceneManager.LoadScene("Zero");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void StartFirstScene()
    {
        SceneManager.LoadScene("First");
    }

}
