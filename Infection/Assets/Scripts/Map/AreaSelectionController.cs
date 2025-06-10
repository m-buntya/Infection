//Chat GPT参照

using UnityEngine;
using UnityEngine.SceneManagement;


/// エリア選択の挙動を制御するコントローラー
public class AreaSelectionController : MonoBehaviour
{
    [Header("クリック検出に使うカメラ")]
    [SerializeField] private Camera mainCamera;

    [Header("エリアのレイヤーマスク")]
    [SerializeField] private LayerMask areaLayerMask;

    // Awakeでカメラの初期化を行う
    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    // 毎フレーム、左クリックを検知してエリアクリックを判定する
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DetectAreaClick();
        }
    }

    
    /// エリアをクリックしたか判定し、選択したらシーンをロードする
    private void DetectAreaClick()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(
            mousePosition,
            Vector2.zero,
            Mathf.Infinity,
            areaLayerMask
        );

        if (hit.collider != null)
        {
            string areaName = hit.collider.gameObject.name;
            Debug.Log($"選択されたエリア: {areaName}");

            // PlayerPrefsに保存
            PlayerPrefs.SetString("SelectedArea", areaName);
            PlayerPrefs.Save();

            LoadSelectedAreaScene(areaName);
        }
    }

    
    /// 指定したエリア名のシーンが存在すればロードする
    private void LoadSelectedAreaScene(string areaName)
    {
        if (Application.CanStreamedLevelBeLoaded(areaName))
        {
            SceneManager.LoadScene(areaName);
        }
        else
        {
            Debug.LogError($"シーン '{areaName}' が Build Settings に登録されていません。");
        }
    }
}
