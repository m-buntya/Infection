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
    void Awake()
    {
        if (iconImage == null)
            iconImage = GetComponentInChildren<Image>();
    }
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
    public void SetUnit(UnitController controller, Sprite placeholder)
    {
        unitController = controller;
        var icon = controller?.GetIconSprite();
        if (iconImage != null)
        {
            iconImage.enabled = true;
            iconImage.sprite = icon ?? placeholder;

            Debug.Log($"🖼️ SetUnit: 表示中の画像 = {iconImage.sprite?.name}");
        }
    }
}
