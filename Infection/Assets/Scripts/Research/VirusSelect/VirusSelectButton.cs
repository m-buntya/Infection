using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

public class VirusSelectButton : MonoBehaviour
{
    [SerializeField] private Image iconImage;

    private CharacterData characterData;
    private VirusSelectWindow parentWindow;
    private Sprite characterIcon;                // 仮置き

    //ID
    public int ID { get; private set; }

    // 仮置き
    public void Setup(Sprite icon, VirusSelectWindow window)// 仮置き
    {                                                       // 仮置き
        characterIcon = icon;                               // 仮置き
        iconImage.sprite = icon;                            // 仮置き
        parentWindow = window;                              // 仮置き
    }                                                       // 仮置き

    /*実装用
    //ボタン初期化
    public void Setup(CharacterData data, VirusSelectWindow window)
    {
        characterData = data;
        iconImage.sprite = data.icon;
        parentWindow = window;
    }*/

    //番号を取得
    public void SetID(int id)
    {
        ID = id;
    }

    // 仮置き
    public void OnClick()                          // 仮置き
    {                                              // 仮置き
        parentWindow.OnSelect(characterIcon, ID);  // 仮置き
    }                                              // 仮置き

    /*実装用
    // 選択したものを返す
    public void OnClick()
    {
        parentWindow.OnSelect(characterData, ID);
    }*/
}
