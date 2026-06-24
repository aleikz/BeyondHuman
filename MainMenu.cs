using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void GoToAbilitySelect()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void GoToGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
