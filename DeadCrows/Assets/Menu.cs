using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
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
