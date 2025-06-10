using UnityEngine;
using UnityEngine.SceneManagement;
public class ScreenManager : MonoBehaviour
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
        if (currentScene.name == "TitleScene" || currentScene.name == "HomeScene" || currentScene.name == "ResultScene")
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
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
