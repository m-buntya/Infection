using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Collections.Generic;
using StatePatteren.State;
public class InitialPanelController :MonoBehaviour
{
    [Header("戻るボタンで表示するダイアログ")]
    public GameObject backConfirmDialog;

    [Header("切り替えるシーン名")]
    public string nextSceneName;

    public List<UnitSlotButton> unitSlotButtons;

    private Dictionary<UnitSlotButton, UnitController> initialSnapshot = new();


    void Start()
    {
        foreach (var slot in unitSlotButtons)
        {
            initialSnapshot[slot] = slot.unitController;
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
        foreach (var slot in unitSlotButtons)
        {
            if (initialSnapshot.ContainsKey(slot))
            {
                slot.unitController = initialSnapshot[slot];

                if (slot.iconImage != null)
                {
                    var icon = TryGetUnitIcon(slot.unitController);
                    if (icon != null)
                    {
                        slot.iconImage.sprite = icon;
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

        SceneManager.LoadScene(nextSceneName);
    }
    private bool IsUnitChanged()
    {
        foreach (var slot in unitSlotButtons)
        {
            if (!initialSnapshot.ContainsKey(slot)) continue;

            var initial = initialSnapshot[slot];
            var current = slot.unitController;

            if (initial != current)
                return true;
        }
        return false;
    }
    private Sprite TryGetUnitIcon(UnitController controller)
    {
        if (controller == null) return null;

        var stats = controller.unitStats;
        if (stats != null)
        {
            var spriteRenderer = controller.GetComponentInChildren<SpriteRenderer>();
            if (spriteRenderer != null)
                return spriteRenderer.sprite;
        }

        return null;
    }

}
