using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadOptionsMenu()
    {
        SceneManager.LoadScene("OptionsMainMenu"); // Brings the user to the Options Menu
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Brings the user back to the main menu
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game"); // Brings the user back to the game menu
    }
}
