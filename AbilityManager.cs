using UnityEngine;
using UnityEngine.SceneManagement;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager instance;

    // 1 = Fire, 2 = Psychic
    public int selectedElement;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Called by UI buttons in selection scene
    public void SelectFire()
    {
        selectedElement = 1;
        Debug.Log("Fire selected");
        SceneManager.LoadScene(1);
    }

    public void SelectPsychic()
    {
        selectedElement = 2;
        Debug.Log("Psychic selected");
        SceneManager.LoadScene(1);
    }
}