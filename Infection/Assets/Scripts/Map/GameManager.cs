using UnityEngine;

/// ゲーム全体の状態を管理するマネージャー（シングルトン）
/// 出展：ChatGPT生成
public class GameManager : MonoBehaviour
{
    /// シングルトンインスタンス
    public static GameManager Instance { get; private set; }

    /// 選択されたステージのデータ
    public StageData SelectedStageData { get; private set; }


    /// シングルトンの初期化と永続化
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    /// 選択されたステージデータをセットする
    public void SetStageData(StageData data)
    {
        SelectedStageData = data;
    }
}
