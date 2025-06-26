using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

public class ResearchSlotButton : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private VirusSelectWindow virusSelectWindow;
    [SerializeField] private Sprite unselectedIcon;
    [SerializeField] private Image checkCover;
    [SerializeField] MaterialConfirmation materialConfirmation;

    //最初に初期化呼び出し
    void Start()
    {
        ResetSlot();
    }

    //初期化
    public void ResetSlot()
    {
        iconImage.sprite = unselectedIcon;
        Transparency(0.2f);
        checkCover.gameObject.SetActive(true);
    }

    //透明度をいじる
    public void Transparency(float alpha)
    {
        Image[] images = GetComponentsInChildren<Image>(true);

        foreach (var img in images)
        {
            Color c = img.color;
            c.a = alpha;
            img.color = c;
        }
    }

    //スロットを押す
    public void OnClickSlot()
    {
        virusSelectWindow.Open(this);
    }

    //アイコン共有
    public void IconSharing(Image selectedIcon)
    {
        selectedIcon.sprite = iconImage.sprite;
    }

    //仮置き
    public void SetMockCharacter(Sprite icon)       //仮置き
    {                                               //仮置き
        iconImage.sprite = icon;                    //仮置き
        Transparency(1.0f);                         //仮置き
                                                    //仮置き
        if (materialConfirmation.isMaterial == true)//仮置き
        {                                           //仮置き
            checkCover.gameObject.SetActive(false); //仮置き
        }                                           //仮置き
    }                                               //仮置き

    /*実装用
    //選択したキャラクターを受け取り反映
    public void SetCharacter(CharacterData data)
    {
        if (data != null)
        {
            iconImage.sprite = data.icon;
            Transparency(1.0f);

            if(materialConfirmation.isMaterial == true)
            {
                checkCover.gameObject.SetActive(false); 
            }
        }
    }*/
}
