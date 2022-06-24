using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMainScene : MonoBehaviour
{
    public GameObject PauseMenu;
    public Canvas canvas;
    bool paused = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && paused == false)
        {
            PauseMenu.SetActive(true);
            canvas.GetComponent<Canvas>().enabled = true;

        }
        if (Input.GetKeyDown(KeyCode.P) && paused == true)
        {
            PauseMenu.SetActive(false);
            canvas.GetComponent<Canvas>().enabled = false;
        }
    }
    public void StartButton()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Loading");
    }

    public void AboutButton()
    {
        SceneManager.LoadScene(2);
        Debug.Log("About");

    }

    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void Testing()
    {

        Debug.Log("Testing");
    }
    public void ReturnButton()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Loading");
    }
}
