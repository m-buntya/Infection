using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;


public class ScreenManager : MonoBehaviour
{

    public delegate void OnRepositionRequest(Scene scene);
    public static event OnRepositionRequest RepositionEvent;
    [SerializeField] private bool useTapInsteadOfButton = true;

    [SerializeField] Text text;
    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                LoadScene();
            }
        }
    }
    //しょっぱな起動
    void Awake()
    {
        DontDestroyOnLoad(transform.root.gameObject);
        SceneManager.sceneLoaded += SceneCheck;
    }

    //規定シーン以外では非表示
    public void SceneCheck(Scene scene, LoadSceneMode mode)
    {
        // StartButtonUIの表示切り替え
        GameObject startButton = GameObject.FindGameObjectWithTag("StartButtonUI");
        if (scene.name == "TitleScene")
        {
            if (startButton != null) startButton.SetActive(false);
        }
        else if (scene.name == "HomeScene")
        {
            if (startButton != null) startButton.SetActive(true);
        }

        // RepositionUIタグのUIを再配置
        GameObject[] repositionUIs = GameObject.FindGameObjectsWithTag("RepositionUI");
        foreach (GameObject ui in repositionUIs)
        {
            ui.SetActive(true); // 表示
            Positioning(scene); // 位置再計算（必要に応じてuiを渡してもOK）
        }
    }


    //シーン別のポジショニング
    public void Positioning(Scene scene)
    {
        if (scene.name == "TitleScene")
        {
            RectTransform rt = GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0, -140);
            rt.sizeDelta = new Vector2(800, 80);
            text.text = "スタート";
        }
        else if(scene.name == "HomeScene")
        {
            RectTransform rt = GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(750, -430);
            rt.sizeDelta = new Vector2(400, 200);
            text.text = "出撃";
        }
        else if (scene.name == "ResultScene")
        {
            RectTransform rt = GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(795, -430);
            rt.sizeDelta = new Vector2(300, 150);
            text.text = "退出";
        }
    }

    //他シーンへ移動
    public void LoadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "TitleScene"|| currentScene.name == "ResultScene")
        {
            SceneManager.LoadScene("HomeScene");
        }
        else if(currentScene.name == "HomeScene")
        {
            SceneManager.LoadScene("MapScene");
        }
        else
        {
            Debug.Log("確認が必要です");
        }
    }
}
