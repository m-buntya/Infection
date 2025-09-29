using StatePatteren.State;
using UnityEngine;
using UnityEngine.UI;

public class UnitSlotUI : MonoBehaviour
{
    public Image iconImage;

    public void SetUnit(UnitController unit, Sprite icon)
    {
        if (iconImage != null)
        {
            iconImage.sprite = icon;
            iconImage.enabled = icon != null;
        }
    }
}