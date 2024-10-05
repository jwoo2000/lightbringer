using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject levelUpCanvas;

    private bool isPaused = false;

    [SerializeField]
    private GameObject[] levelUpChoicePool;

    void Awake()
    {
        menuCanvas.SetActive(false);
        levelUpCanvas.SetActive(false);
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

    public void levelUp()
    {
        stopTimeShowCursor();
        (GameObject, GameObject, GameObject) choices = genLevelUpChoices();
        levelUpCanvas.GetComponent<LevelUpChoices>().SetChoices(choices.Item1, choices.Item2, choices.Item3);
        levelUpCanvas.SetActive(true);
    }

    public void levelUpChoiceSelected()
    {
        startTimeHideCursor();
        levelUpCanvas.SetActive(false);
    }

    private (GameObject, GameObject, GameObject) genLevelUpChoices()
    {
        GameObject left = levelUpChoicePool[0];
        GameObject middle = levelUpChoicePool[1];
        GameObject right = levelUpChoicePool[2];
        return (left, middle, right);
    }

    private void stopTimeShowCursor()
    {
        Time.timeScale = 0f;  // Pause game time
        Cursor.lockState = CursorLockMode.None;  // Unlock cursor
        Cursor.visible = true;  // Show cursor
        isPaused = true;
    }

    private void startTimeHideCursor()
    {
        Time.timeScale = 1f;  // Resume game time
        Cursor.lockState = CursorLockMode.Locked;  // Lock cursor back for game mode
        Cursor.visible = false;  // Hide cursor
        isPaused = false;
    }

    public void ResumeGame()
    {
        menuCanvas.SetActive(false);
        startTimeHideCursor();
    }

    void PauseGame()
    {
        menuCanvas.SetActive(true);
        stopTimeShowCursor();
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
