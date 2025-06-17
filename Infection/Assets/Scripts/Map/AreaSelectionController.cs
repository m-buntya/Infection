// 出典：ChatGPT参照・生成

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/// UIボタンによるエリア選択を制御するコントローラー
public class AreaSelectionController : MonoBehaviour
{
    [Header("エリアボタンの一覧")]
    [SerializeField] private Button[] areaButtonArray;


    /// 各ボタンにクリックイベントを登録する
    private void Awake()
    {
        foreach (Button button in areaButtonArray)
        {
            button.onClick.AddListener(() => OnAreaButtonClicked(button));
        }
    }


    /// ボタンが押されたときの処理。ボタン内のTextを元にシーンをロードする。
    private void OnAreaButtonClicked(Button button)
    {
        TMP_Text areaText = button.GetComponentInChildren<TMP_Text>();

        if (areaText == null)
        {
            Debug.LogError("ボタンにTextMeshProのテキストが見つかりません。");
            return;
        }

        string areaName = areaText.text.Trim();

        if (string.IsNullOrEmpty(areaName))
        {
            Debug.LogError("エリア名が空です。");
            return;
        }

        PlayerPrefs.SetString("SelectedArea", areaName);
        PlayerPrefs.Save();

        LoadSelectedAreaScene(areaName);
    }


    /// シーンがBuildに登録されていれば読み込む
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
