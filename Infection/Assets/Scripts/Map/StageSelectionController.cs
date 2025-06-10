//Chat GPT参照

using UnityEngine;
using UnityEngine.SceneManagement;


/// ステージ選択の挙動を制御するコントローラー
public class StageSelectionController : MonoBehaviour
{
    [Header("メインカメラ")]
    [SerializeField] private Camera mainCamera;

    [Header("ステージ選択可能なレイヤー")]
    [SerializeField] private LayerMask stageLayerMask;

    private string selectedAreaName;

    // Awakeでカメラ初期化とPlayerPrefsからエリア名を読み込み
    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        selectedAreaName = PlayerPrefs.GetString("SelectedArea", "");

        if (string.IsNullOrEmpty(selectedAreaName))
        {
            Debug.LogWarning("選択されたエリア名が保存されていません。");
            // 必要ならここでエリア選択シーンに戻す処理などを追加
        }
        else
        {
            Debug.Log($"エリア「{selectedAreaName}」が選択されました。ステージ選択を開始してください。");
        }
    }

    // 毎フレーム、左クリックを検知しステージクリックを判定
    private void Update()
    {
        if (string.IsNullOrEmpty(selectedAreaName))
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            DetectStageClick();
        }
    }


    /// ステージをクリックしたか判定し、選択したらシーンをロードする
    private void DetectStageClick()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(
            mousePos,
            Vector2.zero,
            Mathf.Infinity,
            stageLayerMask
        );

        if (hit.collider != null)
        {
            string stageName = hit.collider.gameObject.name;
            Debug.Log($"ステージ「{stageName}」が選択されました。");

            LoadSelectedStageScene(stageName);
        }
    }

    
    /// 指定したステージ名のシーンが存在すればロードする
    private void LoadSelectedStageScene(string stageName)
    {
        if (Application.CanStreamedLevelBeLoaded(stageName))
        {
            SceneManager.LoadScene(stageName);
        }
        else
        {
            Debug.LogError($"シーン '{stageName}' が Build Settings に登録されていません。");
        }
    }
}
