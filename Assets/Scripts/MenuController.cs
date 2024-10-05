using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject levelUpCanvas;

    private bool isPaused = false;

    [SerializeField]
    private GameObject[] levelUpChoicePool = new GameObject[6];
    // 0: movespeed
    // 1: light radius
    // 2: light regen
    // 3: dmg reduction (light integrity)
    // 4: max stamina
    // 5: stamina regen

    public string[] choiceTypes =
    {
        "movespeed",
        "light radius",
        "light regen",
        "light integrity",
        "max stamina",
        "stamina regen"
    };

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
        (GameObject left, int leftType, GameObject mid, int midType, GameObject right, int rightType) choices = genLevelUpChoices();
        //Debug.Log("menucontroller: sending choices to levelupchoices");
        levelUpCanvas.GetComponent<LevelUpChoices>().SetChoices(
            choices.left, choices.leftType, 
            choices.mid, choices.midType, 
            choices.right, choices.rightType
            );
        levelUpCanvas.SetActive(true);
    }

    public void levelUpChoiceSelected()
    {
        //Debug.Log("choice selected, menu controller closing level up ui");
        levelUpCanvas.SetActive(false);
        startTimeHideCursor();
    }

    private (GameObject, int, GameObject, int, GameObject, int) genLevelUpChoices()
    {
        //Debug.Log("generating choices");
        int[] possibleChoices = new int[levelUpChoicePool.Length];
        // populate possible choices
        for (int i = 0; i < possibleChoices.Length; i++)
        {
            possibleChoices[i] = i;
        }
        // fisher-yates shuffle
        for (int i = possibleChoices.Length - 1; i>0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = possibleChoices[i];
            possibleChoices[i] = possibleChoices[j];
            possibleChoices[j] = temp;
        }

        GameObject left = levelUpChoicePool[possibleChoices[0]];
        GameObject middle = levelUpChoicePool[possibleChoices[1]];
        GameObject right = levelUpChoicePool[possibleChoices[2]];
        return (left, possibleChoices[0], middle, possibleChoices[1], right, possibleChoices[2]);
    }

    private void stopTimeShowCursor()
    {
        //Debug.Log("stopping time");
        Time.timeScale = 0f;  // Pause game time
        Cursor.lockState = CursorLockMode.None;  // Unlock cursor
        Cursor.visible = true;  // Show cursor
        isPaused = true;
    }

    private void startTimeHideCursor()
    {
        //Debug.Log("resuming time");
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
