using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;


public class TapToChangeScene : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "HomeScene"; //遷移先のシーン名
    [SerializeField] private TextMeshProUGUI tapText;

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (tapText != null)
        {
            if (currentScene.name == "TitleScene")
            {
                tapText.text = "StartGame!";
                nextSceneName = "HomeScene";
            }
            else if (currentScene.name == "HomeScene")
            {
                tapText.text = "Go!";
                nextSceneName = "MapScene";
            }
            else if (currentScene.name == "ResultScene")
            {
                tapText.text = "Back!";
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
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                LoadSceneIfValid();
            }

            // UI上のタップなら無視する
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            //if (!string.IsNullOrEmpty(nextSceneName))
            //{
            //    SceneManager.LoadScene(nextSceneName);
            //}
        }
    }
    private void LoadSceneIfValid()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }

    }
}
