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

    [Header("未選択時に表示する仮アイコン")]
    public Sprite placeholderSprite;

    public List<UnitSlotButton> unitSlotButtons;

    private Dictionary<UnitSlotButton, UnitController> initialSnapshot = new();


    void Start()
    {
        LoadUnitFormationFromManager();
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
        try
        {
            SaveUnitFormation(); // 🔽 これを追加！
            UnitFormationManager.SaveFormation(unitSlotButtons);
            SceneManager.LoadScene(nextSceneName);
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"❌ SaveFormation 中にエラー発生: {ex.Message}\n{ex.StackTrace}");
        }
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
        if (controller == null)
        {
            Debug.Log("ユニットが null です");
            return null;
        }

        if (controller == null) return null;

        var spriteRenderer = controller.GetComponentInChildren<SpriteRenderer>();
        return spriteRenderer?.sprite;

    }

    public void SaveUnitFormation()
    {
        for (int i = 0; i < unitSlotButtons.Count; i++)
        {
            var slot = unitSlotButtons[i];
            var controller = slot.unitController;

            if (controller != null)
            {
                string code = slot.unitCode;
                PlayerPrefs.SetString($"unit_slot_{i}", code);

                var icon = slot.iconImage?.sprite;
                ImageStorageManager.SaveIconName(i, icon);

                // 🔍 ログ追加
                string unitName = controller.unitStats?.unitName ?? "null";
                string iconLabel = icon != null ? icon.name : "null";
                //Debug.Log($"💾 保存: slot[{i}] unitCode = {code}, unitName = {unitName}, icon = {iconLabel}");
            }
            else
            {
                PlayerPrefs.DeleteKey($"unit_slot_{i}");
                ImageStorageManager.ClearIcon(i);

                // 🔍 空スロットログ
                //Debug.Log($"💾 保存: slot[{i}] は空です（削除）");
            }
        }

        PlayerPrefs.Save();
    }
    private void LoadUnitFormationFromManager()
    {
        var formation = UnitFormationManager.GetFormation();

        for (int i = 0; i < unitSlotButtons.Count; i++)
        {
            var slot = unitSlotButtons[i];

            if (i < formation.slotDataList.Count)
            {
                var data = formation.slotDataList[i];
                var unit = UnitCreator.CreateUnitByCode(data.unitCode);

                // ✅ ここで LoadIcon を使う
                var icon = ImageStorageManager.LoadIcon(i, placeholderSprite);

                // ✅ SetUnit に渡すのは LoadIcon の結果
                slot.SetUnit(unit, icon, data.unitCode);
            }
            else
            {
                slot.SetUnit(null, placeholderSprite);
            }
        }
    }
}
