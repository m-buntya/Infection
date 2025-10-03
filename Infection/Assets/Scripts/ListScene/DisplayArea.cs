using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class DisplayArea : MonoBehaviour
{
    public ListManager listManager;

    public Image displayImage;
    public Sprite testSprite; //テスト用のスクリプト
    public TMPro.TMP_Text nameText;
    public GameObject levelSpace;
    public TMPro.TMP_Text levelNumberText;

    public bool isClick = false;
    private UnitStats currentUnitStats;
    private VirusStats currentVirusStats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        listManager = FindObjectOfType<ListManager>();

        //デフォルトで「?」の画像を表示
        displayImage.sprite = testSprite;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetUnitData(UnitStats unitStats)
    {
        if (unitStats == null)
        {
            Debug.LogWarning("UnitStats が null です");
            return;
        }

        levelSpace.SetActive(true); // ユニットではレベルを表示する
        currentUnitStats = unitStats;
        currentVirusStats = null;

        nameText.text = unitStats.unitName;
        levelNumberText.text = $"{unitStats.lv}";
    }

    public void SetVirusData(VirusStats virusStats)
    {
        if (virusStats == null)
        {
            Debug.LogWarning("VirusStats が null です");
            return;
        }

        levelSpace.SetActive(false); // ウイルスではレベルを非表示にする
        currentVirusStats = virusStats;
        currentUnitStats = null;

        nameText.text = virusStats.name;
    }


    //ユニット、ウイルスの詳細ボタンを押したとき
    public void DetailsButton()
    {
        isClick = true;
        listManager.listCanvas.SetActive(false);
        listManager.detailsCanvas.SetActive(true);

        //詳細画面に数値を反映
        var detailsArea = listManager.detailsCanvas.GetComponent<DetailsArea>();
        if (detailsArea == null)
        {
            Debug.LogError("DetailsArea が取得できません！");
            return;
        }

        if (currentUnitStats != null) //ユニットの時
        {
            //detailsArea.CharacterImage.sprite = //ユニットの画像を反映
            detailsArea.costText.text = currentUnitStats.cost.ToString();
            detailsArea.nameText.text = currentUnitStats.unitName;
            detailsArea.levelText.text = "Lv.\n" + currentUnitStats.lv.ToString();
            detailsArea.roleText.text = currentUnitStats.role.ToString();
            detailsArea.leaderSkillText.text = currentUnitStats.leaderSkill.ToString();
            detailsArea.hpText.text = "HP:" + currentUnitStats.maxHp.ToString();
            detailsArea.viursPointText.text = "感染ゲージ:" + currentUnitStats.virusPoint.ToString();
            detailsArea.atkText.text = "攻撃力:" + currentUnitStats.atk.ToString();
            detailsArea.viursPowText.text = "感染力:" + currentUnitStats.virusPow.ToString();
            detailsArea.atkSpdText.text = "攻撃速度:" + currentUnitStats.atkSpd.ToString();
            detailsArea.spdText.text = "移動速度:" + currentUnitStats.spd.ToString();
            detailsArea.rangeText.text = "射程距離:" + currentUnitStats.range.ToString();

            //後で消すかもしれない要素
            detailsArea.explanation.text = " ";
        }
        else if (currentVirusStats != null) //ウイルスの時
        {
            //detailsArea.CharacterImage.sprite = //ウイルスの画像を反映
            detailsArea.nameText.text = currentVirusStats.name;
            detailsArea.hpText.text = "HP:" + currentVirusStats.hp.ToString();
            detailsArea.atkText.text = "攻撃力:" + currentVirusStats.atk.ToString();
            detailsArea.viursPowText.text = "感染力:" + currentVirusStats.virusPow.ToString();
            detailsArea.atkSpdText.text = "攻撃速度:" + currentVirusStats.atkSpd.ToString();
            detailsArea.spdText.text = "移動速度:" + currentVirusStats.spd.ToString();
            detailsArea.rangeText.text = "射程距離:" + currentVirusStats.range.ToString();

            //後で消すかもしれない要素
            detailsArea.explanation.text = currentVirusStats.explanation;
        }
        else
        {
            Debug.LogError("ユニットもウイルスの情報が設定されていません！");
        }
    }
}
