using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FormationPanelManager : MonoBehaviour
{
    public List<FormationUnitButton> formationButtons;

    public Text regularDescriptionText;
    public Text virusDescriptionText;

    public GameObject regularConfirmDialogPanel;
    public GameObject virusConfirmDialogPanel;

    public Button decisionButton;

    public GameObject unitPanel;
    public GameObject virusPanel;

    private UnitData initiallySelectedUnit;
    private UnitSlotButton sourceUnitButton;
    private UnitData currentlySelectedUnit;

    private void Start()
    {
        if (decisionButton != null)
        {
            decisionButton.onClick.AddListener(() => ConfirmSelection());
        }

        regularConfirmDialogPanel?.SetActive(false);
        virusConfirmDialogPanel?.SetActive(false);
    }

    public void ShowFormationPanel(UnitData initialUnit, UnitSlotButton sourceButton)
    {
        initiallySelectedUnit = initialUnit;
        sourceUnitButton = sourceButton;
        currentlySelectedUnit = initialUnit;

        if (initialUnit.unitType == UnitType.Virus)
        {
            unitPanel.SetActive(false);
            virusPanel.SetActive(true);
        }
        else
        {
            virusPanel.SetActive(false);
            unitPanel.SetActive(true);
        }

        HighlightUnit(initialUnit);
    }

    public void HighlightUnit(UnitData selectedUnit)
    {
        currentlySelectedUnit = selectedUnit;

        foreach (var btn in formationButtons)
        {
            btn.SetRedFrameVisible(btn.unitData == selectedUnit);
        }

        if (selectedUnit.unitType == UnitType.Virus)
        {
            if (virusDescriptionText != null)
                virusDescriptionText.text = FormatUnitText(selectedUnit);
            if (regularDescriptionText != null)
                regularDescriptionText.text = "";
        }
        else
        {
            if (regularDescriptionText != null)
                regularDescriptionText.text = FormatUnitText(selectedUnit);
            if (virusDescriptionText != null)
                virusDescriptionText.text = "";
        }
    }

    private string FormatUnitText(UnitData data)
    {
        return
            "ユニット名: " + data.unitName + "\n" +
            "コスト: " + data.cost + "\n" +
            "攻撃力: " + data.attackPower + "\n\n" +
            data.unitDescription;
    }

    public void ConfirmSelection()
    {
        if (sourceUnitButton != null && currentlySelectedUnit != null)
        {
            sourceUnitButton.assignedUnit = currentlySelectedUnit;
            sourceUnitButton.iconImage.sprite = currentlySelectedUnit.Icon;
        }

        regularConfirmDialogPanel?.SetActive(false);
        virusConfirmDialogPanel?.SetActive(false);
        sourceUnitButton?.toggler?.BackToCommon();
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
        regularConfirmDialogPanel?.SetActive(false);
        virusConfirmDialogPanel?.SetActive(false);
        sourceUnitButton?.toggler?.BackToCommon();
    }

    public void CloseConfirmDialog()
    {
        regularConfirmDialogPanel?.SetActive(false);
        virusConfirmDialogPanel?.SetActive(false);
    }

    private void ShowConfirmBackDialog()
    {
        if (currentlySelectedUnit.unitType == UnitType.Virus)
        {
            virusConfirmDialogPanel?.SetActive(true);
        }
        else
        {
            regularConfirmDialogPanel?.SetActive(true);
        }
    }
}