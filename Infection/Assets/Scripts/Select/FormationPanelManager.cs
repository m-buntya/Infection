using StatePatteren.State;
using System.Collections.Generic;
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

    public UnitController unitController;

    private UnitController initiallySelectedUnit;
    private UnitSlotButton sourceUnitButton;
    private UnitController currentlySelectedUnit;

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
            btn.SetRedFrameVisible(btn.unitcontroller == selectedUnit);
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
        var stats = unitController.unitStats;

        return
        $"ユニット名: {stats.unitName}\n" +
        $"コスト: {stats.cost}\n" +
        //$"攻撃力: {stats.attackPower}\n\n" +
        //$"{stats.unitDescription}" +
        $"";
    }


    public void ConfirmSelection()
    {
        if (sourceUnitButton != null && currentlySelectedUnit != null)
        {

            sourceUnitButton.unitController = currentlySelectedUnit;

            sourceUnitButton.iconImage.sprite = TryGetUnitIcon(currentlySelectedUnit);

        }

        CloseConfirmDialog();
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

}