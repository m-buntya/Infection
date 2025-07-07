using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UnitButtonManager : MonoBehaviour
{
    [System.Serializable]
    public class UnitButtonEntry
    {
        public Button button;           // ボタンそのもの
        public UnitData unitData;       // 割り当てられたユニットデータ
        public Image iconImage;         // アイコン画像表示用（省略可）
    }

    [SerializeField] private UnitButtonEntry[] unitButtons;

    public System.Action<UnitData> onUnitSelected; // ユニットが選ばれたときの通知

    private void Start()
    {
        foreach (var entry in unitButtons)
        {
            if (entry.button != null && entry.unitData != null)
            {
                var unit = entry.unitData; // クロージャキャプチャ回避
                entry.button.onClick.AddListener(() => HandleClick(unit));

                if (entry.iconImage != null && unit.icon != null)
                    entry.iconImage.sprite = unit.icon;
            }
        }
    }

    private void HandleClick(UnitData selectedUnit)
    {
        Debug.Log($"ユニット選択: {selectedUnit.unitName}");
        onUnitSelected?.Invoke(selectedUnit);
    }
}