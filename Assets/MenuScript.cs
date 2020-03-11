using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuScript : MonoBehaviour
{
    Text MainText;

    [SerializeField] private bool paused = true;
    GameObject[] UIs;
    void Awake()
    {
        UIs = GameObject.FindGameObjectsWithTag("UI");
        Resume();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(paused == true)
            {
                Resume();
                paused = false;
            }
            else
            {
                Pause();
                paused = true;
            }
        }
    }
    public void Pause()
    {
        paused = true;
        Time.timeScale = 0f;
        foreach(GameObject ui in UIs)
        {
            ui.SetActive(true);
        }
    }
    public void Resume()
    {
        paused = false;
        Time.timeScale = 1f;
        foreach (GameObject ui in UIs)
        {
            ui.SetActive(false);
        }
    }
    public void Exit()
    {
        Application.Quit(); //exit
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
