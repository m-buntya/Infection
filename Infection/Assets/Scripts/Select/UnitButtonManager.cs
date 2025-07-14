using UnityEngine;
using UnityEngine.UI;

public class UnitButtonManager : MonoBehaviour
{
    [System.Serializable]
    public class UnitButtonEntry
    {
        public Button button;               // ボタンそのもの
        public GameObject unitObject;      // UnitController がアタッチされた GameObject
        public Image iconImage; // 表示先の Image コンポーネント
        public Sprite icon;     // 表示する画像（アイコン）

    }

    [SerializeField] private UnitButtonEntry[] unitButtons;

    public System.Action<GameObject> onUnitSelected; // GameObject を通知（UnitControllerに触れない）

    private void Start()
    {
        foreach (var entry in unitButtons)
        {
            if (entry.button != null && entry.unitObject != null)
            {
                var obj = entry.unitObject; // クロージャキャプチャ回避
                entry.button.onClick.AddListener(() => HandleClick(obj));


                if (entry.iconImage != null && entry.icon != null)
                    entry.iconImage.sprite = entry.icon;

            }
        }
    }

    private void HandleClick(GameObject selectedUnitObject)
    {
        Debug.Log($"ユニット選択: {selectedUnitObject.name}");
        onUnitSelected?.Invoke(selectedUnitObject);
    }
}
