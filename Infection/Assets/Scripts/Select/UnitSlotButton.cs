using UnityEngine;
using UnityEngine.UI;

public class UnitSlotButton : MonoBehaviour
{
    public Button button;
    public Image iconImage;
    public UnitData assignedUnit;

    public ObjectToggler toggler;
    public GameObject targetPanelToShow;
    public FormationPanelManager formationPanelManager;

    private void Start()
    {
        if (assignedUnit != null && iconImage != null)
            iconImage.sprite = assignedUnit.icon;

        button.onClick.AddListener(() =>
        {
            if (toggler != null && targetPanelToShow != null && assignedUnit != null)
            {
                toggler.ShowPanelWithUnit(targetPanelToShow, assignedUnit.icon);

                // 編成画面に初期ユニットとこのボタンを渡す
                formationPanelManager.ShowFormationPanel(assignedUnit, this);
            }
        });
    }
}
