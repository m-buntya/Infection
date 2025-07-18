using UnityEngine;
using UnityEngine.UI;
using StatePatteren.State;

public class UnitSlotButton : MonoBehaviour
{
    public UnitController unitController;

    public Button button;
    public Image iconImage;

    public ObjectToggler toggler;
    public GameObject targetPanelToShow;
    public FormationPanelManager formationPanelManager;

    private void Start()
    {

        if (unitController != null && iconImage != null)
        {
            Sprite icon = TryGetUnitIcon(unitController);
            if (icon != null)
                iconImage.sprite = icon;
            iconImage.enabled = true;
        }

        button.onClick.AddListener(() =>
        {
            if (toggler != null && targetPanelToShow != null)
            {
                Sprite icon = TryGetUnitIcon(unitController);
                toggler.ShowPanelWithUnit(targetPanelToShow, icon);

                formationPanelManager.ShowFormationPanel(unitController, this);
            }
        });
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
