using UnityEngine;

public class Exit : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting..."); // This will only show in the editor.
    }
}
