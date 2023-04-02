using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public static int _chosenPlayer;

    public void AmyChoose()
    {
        _chosenPlayer = 0;
        SceneManager.LoadScene(1);
    }

    public void AjChoose()
    {
        _chosenPlayer = 1;
        SceneManager.LoadScene(1);
    }
}
