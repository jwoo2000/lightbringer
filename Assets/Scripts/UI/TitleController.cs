using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    [SerializeField] Transform mainCameraTransform;

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (helpOpen)
            {
                closeHelp();
            }
            else if (optionsOpen)
            {
                closeOptions();
            }
        }
    }

    public void openOptions()
    {
        optionsOpen = true;
        optionsCanvas.SetActive(true);
    }

    public void closeOptions()
    {
        optionsOpen = false;
        optionsCanvas.SetActive(false);
    }

    public void openHelp()
    {
        helpOpen = true;
        helpCanvas.SetActive(true);
    }

    public void closeHelp()
    {
        helpOpen = false;
        helpCanvas.SetActive(false);
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
