using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionManager : MonoBehaviour
{
    public static OptionManager Instance;

    public Canvas optionCanvasPrefab;
    private Canvas optionCanvas;

    private bool isInstantiate = false;

    public RectTransform OptionButton;
    //しょっぱな起動
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 既に存在する場合は破棄
            return;
        }

        Instance = this;
        DontDestroyOnLoad(transform.root.gameObject); // このゲームオブジェクトを破棄しない

        SceneManager.sceneLoaded += SceneCheck;
    }
    
    void Start()
    {
        if (!isInstantiate)
        {
            CreateOptionCanvas();
        }
        // 例：OptionButton を 100px 上に移動する
        RectTransform buttonRect = OptionButton.GetComponent<RectTransform>();
        Vector2 currentPos = buttonRect.anchoredPosition;
        buttonRect.anchoredPosition = new Vector2(currentPos.x, currentPos.y + 100);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (optionCanvas == null)
        {
            CreateOptionCanvas();
        }
        else
        {
            optionCanvas.gameObject.SetActive(false);
        }

        if (scene.name == "TitleScene" || scene.name == "HomeScene")
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
        Positioning(scene);

    }
    //規定シーン以外では非表示
    public void SceneCheck(Scene scene, LoadSceneMode mode)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "TitleScene" || currentScene.name == "HomeScene")
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
        Positioning(scene);
    }

    private void CreateOptionCanvas()
    {
        optionCanvas = Instantiate(optionCanvasPrefab);
        optionCanvas.transform.localScale = Vector3.one;

        RectTransform rt = optionCanvas.GetComponent<RectTransform>();
        rt.anchoredPosition = Vector2.zero;
        rt.sizeDelta = new Vector2(500, 500);

        // Canvas自体もDontDestroyOnLoadにすれば次のシーンでも残せる（好みに応じて）
        DontDestroyOnLoad(optionCanvas.gameObject);

        optionCanvas.gameObject.SetActive(false);
    }

    //オプション画面を開く   
    public void OnOptionCanvas()
    {
        optionCanvas.gameObject.SetActive(true);
    }

    private void Positioning(Scene scene)
    {
        RectTransform rt = GetComponent<RectTransform>();
        if (scene.name == "TitleScene")
        {
            rt.anchoredPosition = new Vector2(800, -430);
            rt.sizeDelta = new Vector2(300, 200);
        }
        else if (scene.name == "HomeScene")
        {
            rt.anchoredPosition = new Vector2(650, 460);
            rt.sizeDelta = new Vector2(200, 140);
        }
    }
}
