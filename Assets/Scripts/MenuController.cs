using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;

    private bool isPaused = false;

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
        Time.timeScale = 1f; 
        isPaused = false;
    }

    void PauseGame()
    {
        menuCanvas.SetActive(true);
        Time.timeScale = 0f;  
        isPaused = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenOptions()
    {
        Debug.Log("Options Menu Opened");
    }
}
