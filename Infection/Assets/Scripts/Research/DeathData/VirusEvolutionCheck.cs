using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirusEvolutionCheck : MonoBehaviour
{
    [SerializeField] ResearchSlotButton researchSlotButton;
    [SerializeField] VirusSelectWindow virusSelectWindow;
    [SerializeField] Image VirusImage;

    //呼び出し
    public void CheckOnClick()
    {
        gameObject.transform.SetAsLastSibling();
        gameObject.SetActive(true);
        VirusIcon();
    }

    //アイコン設定
    public void VirusIcon()
    {
        researchSlotButton.IconSharing(VirusImage);
    }

    //作成
    public void OnClickEvolutionCheck()
    {
        researchSlotButton.ResetSlot();
        Close();
    }

    //閉じる
    public void Close()
    { 
        gameObject.SetActive(false);
    }
}
