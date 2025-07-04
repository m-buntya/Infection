using UnityEngine;
using UnityEngine.UI;

public class ObjectToggler : MonoBehaviour
{
    [System.Serializable]
    public class ToggleEntry
    {
        public GameObject targetToShow;       // 表示させたいUIパネル
        public Image unitIconDisplay;         // ユニットの画像を表示するImage
    }

    [SerializeField] private ToggleEntry[] entries;
    [SerializeField] private GameObject commonToHide;
    [SerializeField] private Button[] backButtons;

    private void Start()
    {
        // 初期化：すべて非表示＋共通UIは表示
        foreach (var entry in entries)
            if (entry.targetToShow != null)
                entry.targetToShow.SetActive(false);

        if (commonToHide != null)
            commonToHide.SetActive(true);

        foreach (var backBtn in backButtons)
            backBtn.onClick.AddListener(BackToCommon);
    }

    public void ShowPanelWithUnit(GameObject targetPanel, Sprite unitIcon)
    {
        foreach (var entry in entries)
        {
            bool active = entry.targetToShow == targetPanel;
            if (entry.targetToShow != null)
                entry.targetToShow.SetActive(active);

            if (active && entry.unitIconDisplay != null && unitIcon != null)
                entry.unitIconDisplay.sprite = unitIcon;
        }

        if (commonToHide != null)
            commonToHide.SetActive(false);
    }

    public void BackToCommon()
    {
        foreach (var entry in entries)
            if (entry.targetToShow != null)
                entry.targetToShow.SetActive(false);

        if (commonToHide != null)
            commonToHide.SetActive(true);
    }
}