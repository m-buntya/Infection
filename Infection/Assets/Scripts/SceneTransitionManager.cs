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

    [Header("シーン遷移ボタン設定")]
    public Button[] sceneButtons;
    public string[] sceneNames;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

        // 画面全体を覆うように設定
        //RectTransform rt = fadeImage.rectTransform;
        //rt.anchorMin = new Vector2(0.5f, 0.5f);
        //rt.anchorMax = new Vector2(0.5f, 0.5f);
        //rt.pivot = new Vector2(0.5f, 0.5f);
        //rt.anchoredPosition = Vector2.zero;
        //rt.sizeDelta = new Vector2(1f, 1f); // 基準サイズ

        //fadeImage.transform.localScale = new Vector3(20f, 12f, 1f); // 拡大倍率

        fadeImage.color = new Color(0, 0, 0, 1);
        fadeImage.gameObject.SetActive(true);
        inputBlocker.SetActive(true);

        // ボタンにイベント登録
        for (int i = 0; i < sceneButtons.Length && i < sceneNames.Length; i++)
        {
            if (string.IsNullOrEmpty(sceneNames[i]))
            {
                Debug.LogError($"sceneNames[{i}] が未設定です！");
                continue;
            }

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
        StartCoroutine(TransitionCoroutine(sceneName));
    }

    private IEnumerator TransitionCoroutine(string nextScene)
    {
        inputBlocker.SetActive(true);
        fadeImage.gameObject.SetActive(true);

        yield return StartCoroutine(SlideFadeOut());
        yield return null;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextScene);
        asyncLoad.allowSceneActivation = false;

        yield return new WaitForSeconds(0.2f);
        asyncLoad.allowSceneActivation = true;
    }

    private IEnumerator SlideFadeOut()
    {
        fadeImage.gameObject.SetActive(true);
        inputBlocker.SetActive(true);

        RectTransform rt = fadeImage.rectTransform;
        float canvasWidth = ((RectTransform)fadeImage.canvas.transform).rect.width;

        // 右端固定、左端は右端からスタート（幅ゼロ）
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(0, 1);
        rt.offsetMax = new Vector2(canvasWidth, 0); // 右端固定
        rt.offsetMin = new Vector2(canvasWidth, 0); // 左端 = 右端 → 幅ゼロ

        fadeImage.color = new Color(0, 0, 0, 1); // 常に不透明

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            float t = elapsed / fadeDuration;
            float leftX = Mathf.Lerp(canvasWidth, 0, t); // 左端を左へスライド

            rt.offsetMin = new Vector2(leftX, 0); // 右端は固定、左端だけ動かす

            elapsed += Time.deltaTime;
            yield return null;
        }

        rt.offsetMin = new Vector2(0, 0); // 最終的に全画面を覆う
    }
    private IEnumerator SlideFadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        inputBlocker.SetActive(true);

        RectTransform rt = fadeImage.rectTransform;
        float canvasWidth = ((RectTransform)fadeImage.canvas.transform).rect.width;

        // 左端固定、右端は画面右端からスタート
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(0, 1);
        rt.offsetMin = new Vector2(0, 0); // 左端固定
        rt.offsetMax = new Vector2(canvasWidth, 0); // 右端は画面右端

        fadeImage.color = new Color(0, 0, 0, 1); // 常に不透明

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            float t = elapsed / fadeDuration;
            float rightX = Mathf.Lerp(canvasWidth, 0, t); // 右端を左へスライド

            rt.offsetMax = new Vector2(rightX, 0); // 左端は固定、右端だけ動かす

            elapsed += Time.deltaTime;
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
        inputBlocker.SetActive(false);
    }
}