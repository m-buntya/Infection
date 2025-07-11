using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Collections.Generic;
public class InitialPanelController :MonoBehaviour
{
    [Header("戻るボタンで表示するダイアログ")]
    public GameObject backConfirmDialog;

    [Header("切り替えるシーン名")]
    public string nextSceneName;

    public List<UnitSlotButton> unitSlotButtons;
    private Dictionary<UnitSlotButton, UnitData> initialSnapshot = new();

    void Start()
    {
        foreach (var slot in unitSlotButtons)
        {
            initialSnapshot[slot] = slot.assignedUnit;
        }
    }
    //戻るボタンを押したときに呼ぶ
    public void OnPressBack()
    {
        if (IsUnitChanged())
            backConfirmDialog?.SetActive(true);
        else
            SceneManager.LoadScene(nextSceneName);
    }

    //決定ボタンを押したときに呼ぶ
    public void OnPressConfirm()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    //ダイアログ内の「いいえ」ボタンを呼ぶ（ダイアログを閉じる）
    public void CloseBackDialog()
    {
        backConfirmDialog?.SetActive(false);
    }

    //ダイアログ内の「はい」ボタンを呼ぶ（シーン移動）
    public void ConfirmBackAndLeaveScene()
    {
        // 🔁 全スロットを初期状態に戻す
        foreach (var slot in unitSlotButtons)
        {
            if (initialSnapshot.ContainsKey(slot))
            {
                slot.assignedUnit = initialSnapshot[slot];

                // アイコンも反映
                if (slot.iconImage != null)
                {
                    if (slot.assignedUnit != null)
                    {
                        slot.iconImage.sprite = slot.assignedUnit.Icon;
                        slot.iconImage.enabled = true;
                    }
                    else
                    {
                        slot.iconImage.sprite = null;
                        slot.iconImage.enabled = false;
                    }
                }
            }
        }

        // 🎬 シーン切り替え
        SceneManager.LoadScene(nextSceneName);
    }
    private bool IsUnitChanged()
    {
        foreach (var slot in unitSlotButtons)
        {
            if (!initialSnapshot.ContainsKey(slot)) continue;

            var initial = initialSnapshot[slot];
            var current = slot.assignedUnit;

            if (initial != current)
                return true;
        }
        return false;
    }
}
