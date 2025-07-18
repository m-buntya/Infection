using UnityEngine;
using System.Collections.Generic;
using StatePatteren.State;
public class UnitSlotSceneInitializer : MonoBehaviour
{
    public List<UnitSlotButton> unitSlotButtons;
    public Sprite placeholderSprite;

    void Start()
    {
        Debug.Log("UnitSlotSceneInitializer 起動");

        var codes = UnitFormationStorage.LoadFormation(unitSlotButtons.Count);

        for (int i = 0; i < unitSlotButtons.Count; i++)
        {
            var slot = unitSlotButtons[i];
            string code = codes[i];
            Debug.Log($"スロット {i} のコード: {code}");

            if (!string.IsNullOrEmpty(code))
            {
                var unit = UnitFactory.CreateUnitByCode(code);
                Debug.Log($"生成されたユニット: {unit?.name}");

                slot.SetUnit(unit, placeholderSprite);
            }
            else
            {
                slot.SetUnit(null, placeholderSprite);
            }
        }

    }
    private Sprite TryGetUnitIcon(UnitController controller)
    {
        if (controller == null) return null;
        var spriteRenderer = controller.GetComponentInChildren<SpriteRenderer>();
        return spriteRenderer?.sprite;
    }
}