using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionExtradition : MonoBehaviour
{

    public static OptionExtradition Instance;
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
        //Positioning(scene);

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
