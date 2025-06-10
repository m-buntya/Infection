using UnityEngine;
using UnityEngine.SceneManagement;
public class ScreenManager : MonoBehaviour
{
    //������ςȋN��
    void Awake()
    {
        DontDestroyOnLoad(transform.root.gameObject);
        SceneManager.sceneLoaded += SceneCheck;
    }

    //�K��V�[���ȊO�ł͔�\��
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

    //���V�[���ֈړ�
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
            Debug.Log("�m�F���K�v�ł�");
        }
    }
}
