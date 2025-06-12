using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class ScreenManager : MonoBehaviour
{
    [SerializeField] Text text;
    //しょっぱな起動
    void Awake()
    {
        DontDestroyOnLoad(transform.root.gameObject);
        SceneManager.sceneLoaded += SceneCheck;
    }

    //規定シーン以外では非表示
    public void SceneCheck(Scene scene, LoadSceneMode mode)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "TitleScene" || currentScene.name == "HomeScene" || currentScene.name == "ResultScene")
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
        Positioning(scene);
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
