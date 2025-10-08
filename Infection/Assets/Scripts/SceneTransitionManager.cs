using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    [Header("フェード設定")]
    public Image fadeImage;
    public GameObject inputBlocker;
    public float fadeDuration = 1f;

    [Header("シーン切り替えボタン")]
    public Button[] sceneButtons;
    public string[] sceneNames;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

        fadeImage.color = new Color(0, 0, 0, 1);
        fadeImage.gameObject.SetActive(true);
        inputBlocker.SetActive(true);

        // ボタンにイベント登録
        for (int i = 0; i < sceneButtons.Length && i < sceneNames.Length; i++)
        {
            string targetScene = sceneNames[i];
            sceneButtons[i].onClick.AddListener(() => RequestSceneChange(targetScene));
        }

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
      
        StartCoroutine(SlideFadeIn());

    }

    public void RequestSceneChange(string sceneName)
    {
        Debug.Log($"[SceneTransition] RequestSceneChange called for: {sceneName}");
        StartCoroutine(TransitionCoroutine(sceneName));

    }

    private IEnumerator TransitionCoroutine(string nextScene)
    {
        inputBlocker.SetActive(true);
        fadeImage.gameObject.SetActive(true);

        yield return StartCoroutine(SlideFadeOut());

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextScene);
        asyncLoad.allowSceneActivation = false;

        yield return new WaitForSeconds(0.2f); // 演出の余韻

        asyncLoad.allowSceneActivation = true;
    }

    IEnumerator SlideFadeOut()
    {
        fadeImage.gameObject.SetActive(true);
        inputBlocker.SetActive(true);

        RectTransform rt = fadeImage.rectTransform;
        float canvasWidth = ((RectTransform)fadeImage.canvas.transform).rect.width;

        // 初期状態：画面右端に配置（幅ゼロ）
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(0, 1);
        rt.offsetMin = new Vector2(canvasWidth, 0);
        rt.offsetMax = new Vector2(canvasWidth, 0);

        fadeImage.color = new Color(0, 0, 0, 1); // 黒・不透明

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            float t = elapsed / fadeDuration;
            float x = Mathf.Lerp(canvasWidth, 0, t); // 右→左へスライド
            rt.offsetMin = new Vector2(x, 0);
            rt.offsetMax = new Vector2(x + canvasWidth, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 最終状態：画面全体を覆う
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(1, 1);
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
    }

    private IEnumerator FadeIn()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            float t = elapsed / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, 1 - t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, 0);
        fadeImage.gameObject.SetActive(false);
        inputBlocker.SetActive(false);
    }
    IEnumerator SlideFadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        inputBlocker.SetActive(true);

        RectTransform rt = fadeImage.rectTransform;

        // 初期状態：画面全体を覆う
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(1, 1);
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            float t = elapsed / fadeDuration;
            float x = Mathf.Lerp(0, -Screen.width, t); // 右端を左へスライド
            rt.offsetMax = new Vector2(x, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 完全に消す
        rt.offsetMax = new Vector2(-Screen.width, 0);
        fadeImage.gameObject.SetActive(false);
        inputBlocker.SetActive(false);
    }
}