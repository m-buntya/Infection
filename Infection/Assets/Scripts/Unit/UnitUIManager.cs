using TMPro;
using UnityEngine;

public class UnitUIManager : MonoBehaviour
{
    [SerializeField] UnitFormation unitFormation;

    [SerializeField] TextMeshProUGUI roleText;
    [SerializeField] TextMeshProUGUI soldierCntText;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI atkText;
    [SerializeField] TextMeshProUGUI virusPowText;
    [SerializeField] TextMeshProUGUI atkSpdText;
    [SerializeField] TextMeshProUGUI spdText;
    [SerializeField] TextMeshProUGUI rangeText;
    [SerializeField] TextMeshProUGUI costText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UnitParaTexts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 部隊のステータス表示
    public void UnitParaTexts()
    {
        roleText.text = unitFormation.unitPara.leaderUnit.role.ToString();
        soldierCntText.text = unitFormation.unitPara.unitMemberCnt.ToString();
        hpText.text = unitFormation.unitPara.leaderUnit.hp.ToString("F1");
        atkText.text = unitFormation.unitPara.leaderUnit.atk.ToString("F1");
        virusPowText.text = unitFormation.unitPara.leaderUnit.virusPow.ToString("F1");
        atkSpdText.text = unitFormation.unitPara.leaderUnit.atkSpd.ToString("F1");
        spdText.text = unitFormation.unitPara.leaderUnit.spd.ToString("F1");

        rangeText.text = unitFormation.unitPara.leaderUnit.range.ToString();
        costText.text = unitFormation.unitPara.leaderUnit.cost.ToString();
    }
}