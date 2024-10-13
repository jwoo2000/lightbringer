using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameWinCanvasController : MonoBehaviour
{
    [SerializeField] private Image bg;
    [SerializeField] private CanvasGroup winContent;
    [SerializeField] private TextMeshProUGUI buttonText;

    [SerializeField] LevelUpgradesUI levelUpgradesUI;
    [SerializeField] WinWeaponInfoPanelController weaponInfoPanelController;
    [SerializeField] GameManager gameManager;

    private Color bgColor;
    private Color buttonColor;
    private void Awake()
    {
        bgColor = bg.color;
        buttonColor = buttonText.color;
    }
    private void Start()
    {
        bg.color = new Color(bg.color.r, bg.color.g, bg.color.b, 0.0f);
        winContent.alpha = 0.0f;
        buttonText.color = new Color(buttonColor.r, buttonColor.g, buttonColor.b, 0.0f);
    }

    private void OnEnable()
    {
        levelUpgradesUI.updateUI();
        weaponInfoPanelController.setWeapons();
        weaponInfoPanelController.updateAllUI();
        StartCoroutine(fadeIn());
    }

    private IEnumerator fadeIn()
    {
        yield return StartCoroutine(FadeInBG(bg, bgColor, 10.0f));
        gameManager.playerMovementController.controlsActive = false;
        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(FadeInCanvasGroup(winContent, 2.0f));
        yield return new WaitForSeconds(2.0f);
        yield return StartCoroutine(FadeInText(buttonText, buttonColor, 3.0f));
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private IEnumerator FadeInCanvasGroup(CanvasGroup cg, float fadeDur)
    {
        float timeElapsed = 0.0f;

        while (timeElapsed < fadeDur)
        {
            cg.alpha = Mathf.Lerp(0.0f, 1.0f, timeElapsed / fadeDur);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        cg.alpha = 1.0f;
    }

    private IEnumerator FadeInBG(Image image, Color ogColor, float fadeDur)
    {
        float timeElapsed = 0.0f;

        while (timeElapsed < fadeDur)
        {
            float currAlpha = Mathf.Lerp(0.0f, 1.0f, timeElapsed / fadeDur);
            image.color = new Color(ogColor.r, ogColor.g, ogColor.b, currAlpha);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        image.color = new Color(ogColor.r, ogColor.g, ogColor.b, 1.0f);
    }

    private IEnumerator FadeInText(TMP_Text text, Color ogColor, float fadeDur)
    {
        float timeElapsed = 0.0f;

        while (timeElapsed < fadeDur)
        {
            float currAlpha = Mathf.Lerp(0.0f, 1.0f, timeElapsed / fadeDur);
            text.color = new Color(ogColor.r, ogColor.g, ogColor.b, currAlpha);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        text.color = new Color(ogColor.r, ogColor.g, ogColor.b, 1.0f);
    }

    // Start is called before the first frame update
    public void restartButton()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene("startScene");
    }
}
