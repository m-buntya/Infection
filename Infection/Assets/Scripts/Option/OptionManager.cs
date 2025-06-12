using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionManager : MonoBehaviour
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

    //�V�[���ʂ̃|�W�V���j���O
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
