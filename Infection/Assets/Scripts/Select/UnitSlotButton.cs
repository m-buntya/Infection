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

    public string unitCode; // ✅ ユニット識別コード（保存用）
    public string iconName; // 保存用
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
    public void SetUnit(UnitController controller, Sprite icon, string code = "")
    {
        if (iconImage == null)
            iconImage = GetComponentInChildren<Image>(); // ✅ 保険として再取得

        unitController = controller;
        unitCode = code;
        iconName = icon != null ? icon.name : "";

        if (iconImage != null)
        {
            iconImage.enabled = true;
            iconImage.sprite = icon;
            //Debug.Log($"🖼️ SetUnit: 表示中の画像 = {iconImage.sprite?.name}");
        }
        else
        {
            Debug.LogWarning("❌ iconImage が取得できませんでした");
        }
        //Debug.Log($"🧪 SetUnit 呼び出し: icon = {icon?.name}, enabled = {iconImage.enabled}, sprite = {iconImage.sprite?.name}");
    }
}
