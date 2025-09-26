using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class TapToChangeScene:MonoBehaviour
{
    [SerializeField] private string nextSceneName = "HomeScene"; //遷移先のシーン名
    [SerializeField] private TextMeshProUGUI tapText;

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (tapText = null)
        {
            if (currentScene.name == "TitleScene")
            {
                tapText.text = "スタート";
                nextSceneName = "HomeScene";
            }
            else if (currentScene.name == "HomeScene")
            {
                tapText.text = "出撃";
                nextSceneName = "MapScene";
            }
            else if (currentScene.name == "ResultScene") 
            {
                tapText.text = "退出";
                nextSceneName = "HomeScene";
            }
            else
            {
                tapText.text = "Tap To Screen!";
                nextSceneName = "";
            }
        }
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    SceneManager.LoadScene(nextSceneName);
        //}
    }

    public void OnTapScreen()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "TitleScene")
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

}
