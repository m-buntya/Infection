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
        LoadUnitFormation();
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
        SaveUnitFormation(); // ←これを有効化
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
            var controller = unitSlotButtons[i].unitController;
            if (controller != null)
            {
               

                var icon = controller.GetIconSprite();
                if (icon != null)
                {
                    string iconName = icon.name;
                    PlayerPrefs.SetString($"unit_icon_{i}", iconName); // アイコン名保存
                }
            }
        }

        PlayerPrefs.Save();
    }
    private void LoadUnitFormation()
    {
        
        for (int i = 0; i < unitSlotButtons.Count; i++)
        {
            var slot = unitSlotButtons[i];
            string code = PlayerPrefs.GetString($"unit_slot_{i}", "");
            string iconName = PlayerPrefs.GetString($"unit_icon_{i}", "");

            if (!string.IsNullOrEmpty(code))
            {
              
                var unit = UnitCreator.CreateUnitByCode(code);

                // Resourcesからアイコンを読み込む
                Sprite icon = null;
                if (!string.IsNullOrEmpty(iconName))
                {
                   
                    icon = Resources.Load<Sprite>($"Icons/{iconName}");
                    if (icon == null)
                        Debug.LogWarning($"❌ Resources.Load 失敗: Icons/{iconName}");
                }

                slot.SetUnit(unit, icon ?? placeholderSprite); // ✅ ここで統一
            }
            else
            {
                slot.SetUnit(null, placeholderSprite); // 空スロットにも対応
            }
        }
    }
}
