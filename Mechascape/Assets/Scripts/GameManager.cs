using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Restart the game
    public void Restart()
    {
        Debug.Log("Game Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
