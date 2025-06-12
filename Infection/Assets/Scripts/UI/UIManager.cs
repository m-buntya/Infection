using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject uiPanel;  // UIのオブジェクト
    public GameObject showUIButton; // UIを表示するボタン

    public void ShowUI()
    {
        uiPanel.SetActive(true); // UIを表示
        showUIButton.SetActive(false); // 表示ボタンを非表示
    }

    public void HideUI()
    {
        uiPanel.SetActive(false); // UIを非表示
        showUIButton.SetActive(true); // 表示ボタンを再表示
    }
}