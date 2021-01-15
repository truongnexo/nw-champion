using UnityEngine;

public class MenuSence : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioSource;
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
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
    }
}
