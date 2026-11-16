using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start Game
    public void PlayGame()
    {
        StartCoroutine(DeleyLoad(2.0f));
    }

    // Quit Game
    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }

    // Load the next scene of the game once you press the start button
    IEnumerator DeleyLoad(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

}
