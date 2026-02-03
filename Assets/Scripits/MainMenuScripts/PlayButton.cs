using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour

{
    public void PlayGame ()
    {
        SceneManager.LoadScene("AstroRepairMap");
    }

    public void QuitGame ()
    {
        Application.Quit();
    }
    public void Prototype()
    {
        SceneManager.LoadScene("TestScene");

    }
}

