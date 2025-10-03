using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class VirusSelectWindow : MonoBehaviour
{
    [SerializeField] private GameObject virusButtonPrefab;
    [SerializeField] private int counter = 0;
    [SerializeField] public int selectID;
    [SerializeField] private Transform buttonParent; 
    //[SerializeField] private [] ;
    [SerializeField] VirusChoice virusChoice;
    [SerializeField] private List<Sprite> mockIcons; // 仮置き
    private ResearchSlotButton currentSlot;

    //VirusStats 〇〇〇;
    //〇〇〇 = virusChoice.GetCurrentVirus();
    
    // ウィンドウを開く
    public void Open(ResearchSlotButton caller)
    {
        currentSlot = caller;
        gameObject.SetActive(true);
        gameObject.transform.SetAsLastSibling();  

        GenerateButtons();
    }

    //開きなおす際に一度消して再生成
    private void GenerateButtons()
    {
        counter = 0;
        foreach (Transform child in buttonParent)
        {
            Destroy(child.gameObject);
        }

        //仮置き
        foreach (var icon in mockIcons)                                 //仮置き
        {                                                               //仮置き
            var obj = Instantiate(virusButtonPrefab, buttonParent);     //仮置き
            var button = obj.GetComponent<VirusSelectButton>();         //仮置き
            button.Setup(icon, this);                                   //仮置き
            button.SetID(counter);                                      //仮置き
            counter++;                                                  //仮置き
        }                                                               //仮置き

        /*実装用
        foreach (var character in characterList)
        {
            var buttonObj = Instantiate(virusButtonPrefab, buttonParent);
            var buttonScript = buttonObj.GetComponent<VirusSelectButton>();
            buttonScript.Setup(character, this);
            button.SetID(counter);
            counter++;
        }*/
    }

    //仮置き
    public void OnSelect(Sprite selectedIcon, int id)//仮置き
    {                                                //仮置き
        selectID = id;                               //仮置き
        currentSlot.SetMockCharacter(selectedIcon);  //仮置き
        gameObject.SetActive(false);                 //仮置き
    }                                                //仮置き

    /*実装用
    //キャラ選択で閉じる
    public void OnSelect(CharacterData data)
    {
        currentSlot.SetCharacter(data);  
        gameObject.SetActive(false);     
    }*/

    // 閉じるとき
    public void Close()
    {
        gameObject.SetActive(false);
        currentSlot = null;
    }
}
