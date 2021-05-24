using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainManu : MonoBehaviour
{
    public void OpenSecondLevel()
    {
        SceneManager.LoadScene("SecondLevel");
    }
    
    public void OpenFirstLevel()
    {
        SceneManager.LoadScene("FirstLevel");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("QUIT"); 
    }
    public void OpenMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
