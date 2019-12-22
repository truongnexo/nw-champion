using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSence : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
        Application.LoadLevel("InGame");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Menu()
    {
        Application.LoadLevel("MainMenu");
    }

    public void Guide()
    {
        Application.LoadLevel("Guide");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
