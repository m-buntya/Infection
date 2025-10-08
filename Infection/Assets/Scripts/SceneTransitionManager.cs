using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;
public class SceneTransitionManager:MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void StartSceneTransition(string nextScene)
    {
        StartCoroutine(TransitionCoroutine(nextScene));
    }

    private IEnumerator TransitionCoroutine(string nextScene)
    {
        yield return StartCoroutine(FadeOutLeftToRight());
        yield return SceneManager.LoadSceneAsync(nextScene);
        yield return StartCoroutine(FadeInLeftToRight());
    }
    private IEnumerator FadeOutLeftToRight()
    {
        fadeImage.gameObject.SetActive(true);
        RectTransform rt = fadeImage.rectTransform;
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(0, 1);
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            float t = elapsed / fadeDuration;
            rt.anchorMax = new Vector2(t, 1);
            elapsed += Time.deltaTime;
            yield return null;
        }
        rt.anchorMax = new Vector2(1, 1);
    }

    private IEnumerator FadeInLeftToRight()
    {
        RectTransform rt = fadeImage.rectTransform;
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            float t = 1f - (elapsed / fadeDuration);
            rt.anchorMin = new Vector2(1 - t, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }
        rt.anchorMin = new Vector2(0, 0);
        fadeImage.gameObject.SetActive(false);
    }
}
