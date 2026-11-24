using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void play()
    {
        SceneManager.LoadScene("CharacterSelectTest");
    }

    public void quit()
    {
        Application.Quit();
    }

    public void howToPlay()
    {

    }

}
