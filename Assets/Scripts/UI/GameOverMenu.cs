using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private Image bg;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI buttonText;
    private Color bgColor;
    private Color titleColor;
    private Color buttonColor;
    private void Awake()
    {
        bgColor = bg.color;
        titleColor = titleText.color;
        buttonColor = buttonText.color;
    }

    private void Start()
    {
        bg.color = new Color(bg.color.r, bg.color.g, bg.color.b, 0.0f);
        titleText.color = new Color(titleColor.r, titleColor.g, titleColor.b, 0.0f);
        buttonText.color = new Color(buttonColor.r, buttonColor.g, buttonColor.b, 0.0f);
    }

    private void OnEnable()
    {
        StartCoroutine(fadeIn());
    }

    private IEnumerator fadeIn()
    {
        StartCoroutine(FadeInBG(bg, bgColor, 3.0f));
        StartCoroutine(FadeInText(titleText, titleColor, 3.0f));
        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(FadeInText(buttonText, buttonColor, 3.0f));

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0.0f;
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
