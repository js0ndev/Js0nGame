using UnityEngine;

public class RestartManager : MonoBehaviour
{
    public void RestartGame()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene
        (
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }
}
