using StatePatteren.State;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormationPanelManager : MonoBehaviour
{
    public List<FormationUnitButton> formationButtons;
    public List<UnitSlotButton> slotButtons;
    public List<UnitDragHandler> dragHandlers;

    public Text regularDescriptionText;
    public Text virusDescriptionText;

    public GameObject regularConfirmDialogPanel;
    public GameObject virusConfirmDialogPanel;

    public Button decisionButton;

    public GameObject unitPanel;
    public GameObject virusPanel;

    public UnitController unitController;

    private UnitController initiallySelectedUnit;
    private UnitSlotButton sourceUnitButton;
    private UnitController currentlySelectedUnit;
    private FormationUnitButton currentlySelectedButton;

    private Transform hiddenUnitRoot;

    public Transform HiddenUnitRoot => hiddenUnitRoot;

    public Button sortieButton; //出撃ボタン

    private void Awake()
    {
        var rootObj = GameObject.Find("HiddenUnitRoot");
        if (rootObj == null)
        {
            rootObj = new GameObject("HiddenUnitRoot");
            rootObj.SetActive(false); // ✅ 初期状態で非表示
        }
        hiddenUnitRoot = rootObj.transform;
    }

    private void OnEnable()
    {
        foreach(var btn in formationButtons)
        {
            if (btn.unitcontroller != null)
            {
                btn.unitcontroller.gameObject.SetActive(false);
            }
        }
    }
    private void Start()
    {
        decisionButton?.onClick.AddListener(() => ConfirmSelection());
        regularConfirmDialogPanel?.SetActive(false);
        virusConfirmDialogPanel?.SetActive(false);

    }

    public void ShowFormationPanel(UnitController unitController, UnitSlotButton sourceButton)
    {
        initiallySelectedUnit = unitController;
        sourceUnitButton = sourceButton;
        currentlySelectedUnit = unitController;

        bool isVirus = unitController.GetUnitGroup() == UnitController.UNIT_GROUP.ENEMY;
        unitPanel.SetActive(!isVirus);
        virusPanel.SetActive(isVirus);

        HighlightUnit(unitController); 
    }

    public void HighlightUnit(UnitController selectedUnit)
    {
        currentlySelectedUnit = selectedUnit;

        foreach (var btn in formationButtons)
        {
            bool isSame = btn.unitcontroller != null &&
                          btn.unitcontroller.GetInstanceID() == selectedUnit.GetInstanceID();

            btn.SetRedFrameVisible(isSame);
        }

        string formattedText = FormatUnitText(selectedUnit);
        if (selectedUnit.GetUnitGroup() == UnitController.UNIT_GROUP.ENEMY)
        {
            virusDescriptionText.text = formattedText;
            regularDescriptionText.text = "";
        }
        else
        {
            regularDescriptionText.text = formattedText;
            virusDescriptionText.text = "";
        }
    }




    private string FormatUnitText(UnitController unitController)
    {
        var attackBase = unitController.GetComponent<UnitAttackBace>();
        if (attackBase == null || attackBase.unitStats == null)
            return "ステータス情報が取得できません";

        var stats = attackBase.unitStats;

        return
            $"ユニット名: {stats.unitName}\n" +
            $"レベル: {stats.lv}\n" +
            $"ロール: {ConvertRoleToJapanese(stats.role)}\n" +
            $"攻撃力: {stats.atk}\n" +
            $"コスト: {stats.cost}";
    }
    private string ConvertRoleToJapanese(UnitStats.ROLE role)
    {
        switch (role)
        {
            case UnitStats.ROLE.Attacker: return "アタッカー";
            case UnitStats.ROLE.Tank: return "タンク";
            case UnitStats.ROLE.Healer: return "ヒーラー";
            case UnitStats.ROLE.Baffer: return "バッファー";
            case UnitStats.ROLE.Debaffer: return "デバッファー";
            case UnitStats.ROLE.Archer: return "アーチャー";
            case UnitStats.ROLE.Wizard: return "魔法使い";
            default: return "不明";
        }
    }
    public void ConfirmSelection()
    {
        if (sourceUnitButton != null && currentlySelectedUnit != null)
        {
            var icon = TryGetUnitIcon(currentlySelectedUnit);
            var stats = currentlySelectedUnit.unitStats;
            if (stats != null)
            {
                //Debug.Log($"🧬 unitStats 内容: unitCode = {stats.unitCode}, unitName = '{stats.unitName}', atk = {stats.atk}");
            }
            else
            {
                Debug.LogWarning("⚠️ unitStats が null です");
            }

            if (stats != null)
            {
                var code = stats.unitCode.ToString();
                var name = stats.unitName;

                //Debug.Log($"🧠 ConfirmSelection: unitCode = {code}, unitName = {name}, iconName = {icon?.name}");
                //Debug.Log($"🔍 currentlySelectedUnit = {currentlySelectedUnit.name}, instanceID = {currentlySelectedUnit.GetInstanceID()}");

                UnitFormationManager.SetSlotData(sourceUnitButton.slotIndex, code, icon?.name);


                sourceUnitButton.SetUnit(currentlySelectedUnit, icon, code, name);
            }
            else
            {
                Debug.LogWarning("⚠️ ConfirmSelection: unitStats が null です");
            }
        }

        CloseConfirmDialog();
        sourceUnitButton?.toggler?.BackToCommon();
        UpdateSprtieButtonState();
        UpdateDragAvailability();
        Debug.Log("bbb");
    }

    public void TryGoBack()
    {
        if (currentlySelectedUnit == initiallySelectedUnit)
        {
            sourceUnitButton?.toggler?.BackToCommon();
        }
        else
        {
            ShowConfirmBackDialog();
        }
    }

    public void ConfirmBackAndExit()
    {
        CloseConfirmDialog();
        sourceUnitButton?.toggler?.BackToCommon();
    }

    public void CloseConfirmDialog()
    {
        regularConfirmDialogPanel?.SetActive(false);
        virusConfirmDialogPanel?.SetActive(false);
    }

    private void ShowConfirmBackDialog()
    {
        if (currentlySelectedUnit.GetUnitGroup() == UnitController.UNIT_GROUP.ENEMY)
        {
            virusConfirmDialogPanel?.SetActive(true);
        }
        else
        {
            regularConfirmDialogPanel?.SetActive(true);
        }
    }
    private Sprite TryGetUnitIcon(UnitController controller)
    {
        if (controller == null) return null;

        var spriteRenderer = controller.GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
            return spriteRenderer.sprite;

        return null;
    }
    public UnitSlotButton GetCurrentSlotButton()
    {
        return sourceUnitButton;
    }

   public void UpdateSprtieButtonState()
    {
        bool hasUnit = false;

        foreach(var slot in slotButtons)
        {
            if (slot.unitController != null)
            {
                hasUnit = true;
                break;
            }
        }
        sortieButton.interactable = hasUnit;
    }

    public void UpdateDragAvailability()
    {
        Debug.Log("aaa");
        for(int i = 0; i < dragHandlers.Count; i++)
        {
            bool hasUnit = i < slotButtons.Count && slotButtons[i].unitController != null;
            dragHandlers[i].SetIsDrag(hasUnit);

        }
    }
}