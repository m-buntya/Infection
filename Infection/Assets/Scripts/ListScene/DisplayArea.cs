using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class DisplayArea : MonoBehaviour
{
    public Image displayImage;
    public Sprite testSprite; //テスト用のスクリプト
    public TMPro.TMP_Text nameText;
    public GameObject levelSpace;
    public TMPro.TMP_Text levelNumberText;

    public bool isClick = false;
    private UnitStats currentUnitStats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //デフォルトで「?」の画像を表示
        displayImage.sprite = testSprite;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetUnitData(UnitStats stats)
    {
        levelSpace.SetActive(true); // ユニットではレベルを表示する

        currentUnitStats = stats;

        nameText.text = stats.unitName;
        levelNumberText.text = $"{stats.lv}";
    }

    public void SetVirusData(VirusStats virusStats)
    {
        levelSpace.SetActive(false); // ウイルスではレベルを非表示にする

        if (virusStats == null)
        {
            Debug.LogWarning("VirusStats が null です");
            return;
        }

        nameText.text = virusStats.name;
    }


    
    public void DetailsButton()
    {
        if (currentUnitStats != null)
        {
            isClick = true;
            Debug.Log($"ユニット名:{currentUnitStats.unitName} Lv:{currentUnitStats.lv}");
        }
        else
        {
            Debug.LogWarning("ユニット情報が設定されていません！");
        }
    }
}
