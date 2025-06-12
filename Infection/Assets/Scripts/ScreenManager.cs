using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class ScreenManager : MonoBehaviour
{
    [SerializeField] Text text;
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
        Positioning(scene);
    }

    //�V�[���ʂ̃|�W�V���j���O
    public void Positioning(Scene scene)
    {
        if (scene.name == "TitleScene")
        {
            RectTransform rt = GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0, -140);
            rt.sizeDelta = new Vector2(800, 80);
            text.text = "�X�^�[�g";
        }
        else if(scene.name == "HomeScene")
        {
            RectTransform rt = GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(750, -430);
            rt.sizeDelta = new Vector2(400, 200);
            text.text = "�o��";
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
