using StatePatteren.State;
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
    [Header("未選択時に使う仮アイコン")]
    public Sprite placeholderSprite;
    [SerializeField] private UnitButtonEntry[] unitButtons;

    public System.Action<GameObject> onUnitSelected; // GameObject を通知（UnitControllerに触れない）

    private void Start()
    {
        foreach (var entry in unitButtons)
        {
            if (entry.iconImage != null)
            {
                entry.iconImage.sprite = entry.icon ?? placeholderSprite; // null のときは仮アイコンを表示
                entry.iconImage.enabled = true;
            }

            if (entry.button != null && entry.unitObject != null)
            {
                var obj = entry.unitObject; // クロージャキャプチャ回避
                entry.button.onClick.AddListener(() => HandleClick(obj));
            }
            Sprite finalIcon = entry.icon ?? placeholderSprite;
            entry.iconImage.sprite = finalIcon;
            entry.iconImage.enabled = finalIcon != null;
        }
    }

    private void HandleClick(GameObject selectedUnitObject)
    {
        //Debug.Log($"ユニット選択: {selectedUnitObject.name}");

        // スロット番号をどこかで管理しているならそれを使う（例: selectedSlotIndex）
        //var controller = selectedUnitObject.GetComponent<UnitController>();
        //if (controller != null)
        //{
        //    var stats = controller.unitStats;
        //    PlayerPrefs.SetString($"unit_slot_{selectedSlotIndex}", stats.unitCode.ToString());
        //    PlayerPrefs.Save();
        //}

        onUnitSelected?.Invoke(selectedUnitObject);
    }
}
