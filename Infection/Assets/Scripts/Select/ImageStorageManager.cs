using UnityEngine;

public static class ImageStorageManager
{
    public static void SaveIconName(int slotIndex, Sprite icon)
    {
        string iconName = icon != null ? icon.name : "";
        PlayerPrefs.SetString($"unit_icon_{slotIndex}", iconName);
    }

    public static Sprite LoadIcon(int slotIndex, Sprite fallback)
    {
        string iconName = PlayerPrefs.GetString($"unit_icon_{slotIndex}", "");
        //Debug.Log($"🧪 LoadIcon: slot {slotIndex}, iconName = {iconName}");

        if (!string.IsNullOrEmpty(iconName))
        {
            // ✅ パスを "Sprites/" に変更
            Sprite icon = Resources.Load<Sprite>($"Sprites/{iconName}");
            if (icon != null)
            {
                Debug.Log($"✅ Resources.Load 成功: Sprites/{iconName}");
                return icon;
            }

            Debug.LogWarning($"❌ Resources.Load 失敗: Sprites/{iconName}");
        }
        else
        {
            Debug.Log($"⚠️ PlayerPrefs にアイコン名が保存されていません: slot {slotIndex}");
        }

        return fallback;
    }
    public static void ClearIcon(int slotIndex)
    {
        PlayerPrefs.DeleteKey($"unit_icon_{slotIndex}");
    }
}