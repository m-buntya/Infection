using UnityEngine;
using UnityEngine.SceneManagement;
public class ObjectiveProgressManager : MonoBehaviour
{
    [SerializeField] private bool isTestMode = false; // 🔹 インスペクターでテストモード切り替え

    private int completedObjectives = 0;
    private const int REQUIRED_OBJECTIVES = 3;
    private GameClearController gameClearController;

    private bool aKeyPressed = false;
    private bool bKeyPressed = false;
    private bool cKeyPressed = false;

    void Start()
    {
        gameClearController = FindObjectOfType<GameClearController>();

        if (gameClearController == null)
        {
            Debug.LogError("GameClearController がシーン内に見つかりません！");
        }
    }

    void Update()
    {
        if (isTestMode) // 🔹 インスペクターのフラグが true の場合のみテストコード実行
        {
            CheckKeyPressObjective();
        }
    }

    void CheckKeyPressObjective()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            aKeyPressed = true;
           
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            bKeyPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            cKeyPressed = true;
        }

        if (aKeyPressed && bKeyPressed && cKeyPressed)
        {
            Debug.Log("【テスト】ノルマ達成！");
            UpdateObjectiveProgress();
        }
    }

    public void UpdateObjectiveProgress()
    {
        completedObjectives++;

        if (completedObjectives >= REQUIRED_OBJECTIVES)
        {
            NotifyGameClearController();
        }
    }

    private void NotifyGameClearController()
    {
        if (gameClearController != null)
        {
            Debug.Log("ノルマ達成：ゲームクリア処理を開始");
            gameClearController.CheckGameClearConditions();
            TransitionToGameScene(); // 🔹 ゲームシーンへの移動を実行

        }
        else
        {
            Debug.LogError("GameClearControllerが見つかりません");
        }
    }
    private void TransitionToGameScene()
    {
        Debug.Log("🎮 ゲームシーンへ移動");
        SceneManager.LoadScene("HomeScene"); // 🔹 ノルマ達成後にゲームシーンへ移動
    }

}