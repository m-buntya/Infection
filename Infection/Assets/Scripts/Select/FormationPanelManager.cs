using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormationPanelManager : MonoBehaviour
{
    public List<FormationUnitButton> formationButtons;
    public Text descriptionText;

    private UnitData initiallySelectedUnit;
    private UnitSlotButton sourceUnitButton;
    private UnitData currentlySelectedUnit;
    public Button decisionButton;

    private void Start()
    {
        if (decisionButton != null)
        {
            decisionButton.onClick.AddListener(() => ConfirmSelection());
        }
    }

    // 編成画面を開いたときに呼ばれる
    public void ShowFormationPanel(UnitData initialUnit, UnitSlotButton sourceButton)
    {
        initiallySelectedUnit = initialUnit;
        sourceUnitButton = sourceButton;
        currentlySelectedUnit = initialUnit;

        HighlightUnit(initialUnit);
    }

    // 赤枠と説明文を更新
    public void HighlightUnit(UnitData selectedUnit)
    {
        currentlySelectedUnit = selectedUnit;

        foreach (var btn in formationButtons)
        {
            bool isMatch = btn.unitData == selectedUnit;
            btn.SetRedFrameVisible(isMatch);
        }

        if (descriptionText != null)
        {
            descriptionText.text =
                "ユニット名: " + selectedUnit.unitName + "\n" +
                "コスト: " + selectedUnit.cost + "\n" +
                "攻撃力: " + selectedUnit.attackPower + "\n\n" +
                selectedUnit.unitDescription;
        }
    }

    // 決定ボタンで元のユニットボタンを切り替える
    public void ConfirmSelection()
    {
        if (sourceUnitButton != null && currentlySelectedUnit != null)
        {
            sourceUnitButton.assignedUnit = currentlySelectedUnit;
            sourceUnitButton.iconImage.sprite = currentlySelectedUnit.icon;
        }
    }
}
