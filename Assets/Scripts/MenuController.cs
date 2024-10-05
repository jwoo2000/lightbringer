using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;

    private bool isPaused = false;

    void Awake()
    {
        menuCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        menuCanvas.SetActive(false);
        Time.timeScale = 1f;  // Resume game time
        Cursor.lockState = CursorLockMode.Locked;  // Lock cursor back for game mode
        Cursor.visible = false;  // Hide cursor
        isPaused = false;
    }

    void PauseGame()
    {
        menuCanvas.SetActive(true);
        Time.timeScale = 0f;  // Pause game time
        Cursor.lockState = CursorLockMode.None;  // Unlock cursor
        Cursor.visible = true;  // Show cursor
        isPaused = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OpenOptions()
    {
        Debug.Log("Options Menu Opened");
    }
}
