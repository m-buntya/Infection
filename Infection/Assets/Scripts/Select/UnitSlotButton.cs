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

    public UnitData defaultUnit;
    private void Start()
    {
        if (assignedUnit != null && iconImage != null)
            iconImage.sprite = assignedUnit.icon;

        button.onClick.AddListener(() =>
        {
            if (toggler != null && targetPanelToShow != null)
            {
                toggler.ShowPanelWithUnit(targetPanelToShow, assignedUnit != null ? assignedUnit.icon : null);

                // 空ボタンでも仮の初期値（ダミーデータ）を使って編成画面を開く
                UnitData unitToEdit = assignedUnit ?? GetDefaultUnitForEditing(); // ←ここ大事

                formationPanelManager.ShowFormationPanel(unitToEdit, this);
            }
        });
    }
    private UnitData GetDefaultUnitForEditing()
    {
        // 仮のダミーユニット or ウイルスなど、1つ用意しておくと安心
        return defaultUnit; // 事前にインスペクターで設定しておく
    }
}
