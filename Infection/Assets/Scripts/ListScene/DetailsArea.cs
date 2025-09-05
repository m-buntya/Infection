using UnityEngine;
using TMPro;
using Microsoft.Unity.VisualStudio.Editor;

public class DetailsArea : MonoBehaviour
{
    public ListManager listManager;

    public Image CharacterImage;
    public TMPro.TMP_Text costText;
    public TMPro.TMP_Text nameText;
    public TMPro.TMP_Text levelText;
    public TMPro.TMP_Text roleText;
    public TMPro.TMP_Text leaderSkillText;
    public TMPro.TMP_Text hpText;
    public TMPro.TMP_Text viursPointText;
    public TMPro.TMP_Text atkText;
    public TMPro.TMP_Text viursPowText;
    public TMPro.TMP_Text atkSpdText;
    public TMPro.TMP_Text spdText;
    public TMPro.TMP_Text rangeText;

    //後で消すかもしれない要素
    public TMPro.TMP_Text explanation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ここにウイルス、ユニットで表示する、しない物を分ける
        if(listManager.pageName == "Virus")
        {
            costText.gameObject.SetActive(false);
            levelText.gameObject.SetActive(false);
            roleText.gameObject.SetActive(false);
            leaderSkillText.gameObject.SetActive(false);
            viursPointText.gameObject.SetActive(false);
        }
        else
        {
            costText.gameObject.SetActive(true);
            levelText.gameObject.SetActive(true);
            roleText.gameObject.SetActive(true);
            leaderSkillText.gameObject.SetActive(true);
            viursPointText.gameObject.SetActive(true);
        }
    }
}
