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

    private bool isSelectionChanged = false;
    public GameObject confirmDialogPanel;
    public GameObject warningPanel;

    private void Start()
    {
        if (decisionButton != null)
        {
            decisionButton.onClick.AddListener(() => ConfirmSelection());
        }

        if (confirmDialogPanel != null)
            confirmDialogPanel.SetActive(false); // 初期状態で非表示にする

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

        // 初期ユニットと違うかどうかを判定
        isSelectionChanged = (selectedUnit != initiallySelectedUnit);

        // 赤枠の表示更新
        foreach (var btn in formationButtons)
        {
            btn.SetRedFrameVisible(btn.unitData == selectedUnit);
        }

        // 説明文更新
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
    // ConfirmSelection() のみで assignedUnit を変更するようにする
    public void ConfirmSelection()
    {
        if (sourceUnitButton != null && currentlySelectedUnit != null)
        {
            sourceUnitButton.assignedUnit = currentlySelectedUnit;
            sourceUnitButton.iconImage.sprite = currentlySelectedUnit.icon;
        }

        confirmDialogPanel.SetActive(false);
        sourceUnitButton?.toggler?.BackToCommon();
    }



    public void CancelBack()
    {
        confirmDialogPanel.SetActive(false);
        sourceUnitButton?.toggler?.BackToCommon();
    }

    public void TryGoBack()
    {
        // 何も判定せず、ただダイアログを表示するだけ
        ShowConfirmBackDialog();
    }


    private void ShowWarningPanel()
    {
        if (warningPanel != null)
            warningPanel.SetActive(true);
    }


    private void ShowConfirmBackDialog()
    {
        if (confirmDialogPanel != null)
            confirmDialogPanel.SetActive(true);
    }

}
