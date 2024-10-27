using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    [SerializeField] Transform mainCameraTransform;
    [SerializeField] UISounds uiSounds;

    public GameObject helpCanvas;
    public GameObject optionsCanvas;
    public GameObject loadingCanvas;

    [SerializeField] private bool helpOpen = false;
    [SerializeField] private bool optionsOpen = false;

    void Awake()
    {
        helpCanvas.SetActive(false);
        optionsCanvas.SetActive(false);
        loadingCanvas.SetActive(false);
    }

    private void Start()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)||Input.GetKeyDown(KeyCode.Space))
        {
            if (helpOpen)
            {
                //Debug.Log("close help");
                closeHelp();
                uiSounds.playSelectSFX();
            }
            else if (optionsOpen)
            {
                //Debug.Log("close options");
                closeOptions();
                uiSounds.playSelectSFX();
            }
        }
    }

    public void openOptions()
    {
        optionsOpen = true;
        optionsCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void closeOptions()
    {
        optionsOpen = false;
        optionsCanvas.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void openHelp()
    {
        helpOpen = true;
        helpCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void closeHelp()
    {
        helpOpen = false;
        helpCanvas.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void StartGame()
    {
        StartCoroutine(LoadAsyncScene());
    }

    IEnumerator LoadAsyncScene()
    {
        Time.timeScale = 1.0f;
        loadingCanvas.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("StartScene");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        loadingCanvas.SetActive(false);
    }
}
