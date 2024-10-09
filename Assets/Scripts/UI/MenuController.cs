using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject levelUpCanvas;
    public GameObject weaponGetCanvas;
    public GameObject weaponUpCanvas;

    [SerializeField]
    private LevelUpgradesUI levelUpgradesUI;
    [SerializeField]
    private WeaponInfoPanelController weaponInfoPanelController;

    [SerializeField]
    private LevelUpChoices levelUpChoices;
    [SerializeField]
    private WeaponController weaponController;
    [SerializeField]
    private WeaponGetUIController weaponGetUIController;
    [SerializeField]
    private WeaponUpUIController weaponUpUIController;

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

    [SerializeField]
    public int pendingLevelUps = 0;

    [SerializeField]
    private bool levelUIOpen = false;

    [SerializeField]
    public bool weaponGetUIOpen = false;

    [SerializeField]
    private bool weaponUpUIOpen = false;

    [SerializeField]
    private bool pauseMenuOpen = false;

    void Awake()
    {
        menuCanvas.SetActive(false);
        levelUpCanvas.SetActive(false);
        weaponGetCanvas.SetActive(false);
    }

    void Update()
    {
        // pause game if level ui and weapon up ui is not open
        if (Input.GetKeyDown(KeyCode.Escape) && !levelUIOpen && !weaponGetUIOpen && !weaponUpUIOpen)
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

        // open level up ui if: level ui is not open && weapon ui is not open && pause menu is not open
        if (Input.GetKeyDown(KeyCode.Tab) && !levelUIOpen && !weaponGetUIOpen && !pauseMenuOpen && !weaponUpUIOpen)
        {
            if (levelUpChoices.newChoices)
            {
                processNextLevelUp();
            } else
            {
                openLevelUI();
            }
        } else if ((Input.GetKeyDown(KeyCode.Tab) && levelUIOpen) || (Input.GetKeyDown(KeyCode.Escape) && levelUIOpen))
        {
            // if level ui is open, close and dont reroll new choices
            levelUpChoices.newChoices = false;
            closeLevelUI();
        }
    }

    public void closeLevelUI()
    {
        levelUpCanvas.SetActive(false);
        startTimeHideCursor();
        levelUIOpen = false;
    }

    public void openLevelUI()
    {
        levelUpCanvas.SetActive(true);
        stopTimeShowCursor();
        levelUIOpen = true;
    }

    public void QueueLevelUps(int pendingLevels)
    {
        pendingLevelUps += pendingLevels;
    }

    public void processNextLevelUp()
    {
        if (pendingLevelUps > 0)
        {
            (GameObject left, int leftType, GameObject mid, int midType, GameObject right, int rightType) choices = genLevelUpChoices();
            //Debug.Log("menucontroller: sending choices to levelupchoices");
            levelUpChoices.SetChoices(
                choices.left, choices.leftType, 
                choices.mid, choices.midType, 
                choices.right, choices.rightType
                );
            openLevelUI();
        }
    }

    public void levelUpChoiceSelected()
    {
        //Debug.Log("choice selected, menu controller closing level up ui");
        pendingLevelUps--;
        levelUpChoices.newChoices = true;
        levelUpCanvas.SetActive(false);
        if (pendingLevelUps > 0)
        {
            processNextLevelUp();
        } else
        {
            startTimeHideCursor();
            levelUIOpen = false;
        }
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

    public void newWeaponChoices(Weapon.Tier tier)
    {
        stopTimeShowCursor();
        weaponGetUIOpen = true;
        (GameObject left, GameObject leftPanel, GameObject mid, GameObject midPanel, GameObject right, GameObject rightPanel) choices = weaponController.random3SelectWeapon(tier);
        weaponGetUIController.SetChoices(choices);
        weaponGetCanvas.SetActive(true);
    }

    public void newWeaponChosen()
    {
        weaponGetCanvas.SetActive(false);
        weaponGetUIOpen = false;
        startTimeHideCursor();
    }

    public void upgradeWeapon(Weapon weapon)
    {
        stopTimeShowCursor();
        weaponUpUIOpen = true;
        weaponUpUIController.SetWeapon(weapon);
        weaponUpCanvas.SetActive(true);
    }

    public void wepUpgradeChosen()
    {
        weaponUpCanvas.SetActive(false);
        weaponUpUIOpen = false;
        startTimeHideCursor();
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
        pauseMenuOpen = false;
        startTimeHideCursor();
    }

    void PauseGame()
    {
        menuCanvas.SetActive(true);
        pauseMenuOpen = true;
        levelUpgradesUI.updateUI();
        weaponInfoPanelController.updateAllUI();
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
