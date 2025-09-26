using UnityEngine;
using System.Collections.Generic;
using StatePatteren.State;
public class UnitSlotSceneInitializer : MonoBehaviour
{
    public List<UnitSlotButton> unitSlotButtons;
    public Sprite placeholderSprite;

    void Start()
    {
        var codes = UnitFormationStorage.LoadFormation(unitSlotButtons.Count);

        for (int i = 0; i < unitSlotButtons.Count; i++)
        {
            var slot = unitSlotButtons[i];
            string code = codes[i];
            string iconName = PlayerPrefs.GetString($"unit_icon_{i}", "");

            UnitController unit = null;
            Sprite icon = null;

            if (!string.IsNullOrEmpty(code))
            {
                unit = UnitFactory.CreateUnitByCode(code);
            }

            if (!string.IsNullOrEmpty(iconName))
            {
                icon = Resources.Load<Sprite>($"Icons/{iconName}");
                if (icon == null)
                    Debug.LogWarning($"❌ Resources.Load 失敗: Icons/{iconName}");
            }

            slot.SetUnit(unit, icon ?? placeholderSprite);
        }
    }
    private Sprite TryGetUnitIcon(UnitController controller)
    {
        if (controller == null) return null;
        var spriteRenderer = controller.GetComponentInChildren<SpriteRenderer>();
        return spriteRenderer?.sprite;
    }
}