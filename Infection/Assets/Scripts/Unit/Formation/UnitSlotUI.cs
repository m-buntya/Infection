using StatePatteren.State;
using UnityEngine;
using UnityEngine.UI;

public class UnitSlotUI : MonoBehaviour
{
    public Image iconImage;

    public void SetUnit(UnitController unit, Sprite icon)
    {
        string unitName = unit?.unitStats?.unitName ?? "null";
        string iconLabel = icon != null ? icon.name : "null";
        //Debug.Log($"🧩 UI反映: unit = {unitName}, icon = {iconLabel}");

        if (iconImage != null)
        {
            iconImage.sprite = icon;
            iconImage.enabled = icon != null;
        }
    }
}