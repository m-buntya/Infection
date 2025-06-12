using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionManager : MonoBehaviour
{
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

    //シーン別のポジショニング
    public void Positioning(Scene scene)
    {
        if (scene.name == "TitleScene")
        {
            RectTransform rt = GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(800, -430);
            rt.sizeDelta = new Vector2(300, 200);
        }
        else if (scene.name == "HomeScene")
        {
            RectTransform rt = GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(650, 460);
            rt.sizeDelta = new Vector2(200, 140);
        }
    }
}
