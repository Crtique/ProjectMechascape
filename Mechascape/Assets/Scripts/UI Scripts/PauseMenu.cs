using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas pauseScreen;
    bool paused = false; // bool to see if we are paused which we will always not be

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseScreen.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Pause the game
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (paused)
            {
                Resume();
                paused = false;
            }
            else
            {
                PauseGame();
                paused = true;
            }
        }
    }

    // Resume the game
    public void Resume()
    {
        Time.timeScale = 1.0f;
        pauseScreen.enabled = false;
    }

    // Go back to the main screen but you will just quit the game for now
    public void QuitToTitle()
    {
        Application.Quit();
    }

    // Pause the game
    void PauseGame()
    {
        pauseScreen.enabled = true;
        Time.timeScale = 0f;
    }
}
