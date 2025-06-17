using TMPro;
using UnityEngine;

public class UnitUIManager : MonoBehaviour
{
    [SerializeField] SquadFormation squadFormation;

    [SerializeField] TextMeshProUGUI roleText;
    [SerializeField] TextMeshProUGUI soldierCntText;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI atkText;
    [SerializeField] TextMeshProUGUI virusPowText;
    [SerializeField] TextMeshProUGUI atkSpdText;
    [SerializeField] TextMeshProUGUI spdText;
    [SerializeField] TextMeshProUGUI isFlyText;
    [SerializeField] TextMeshProUGUI rangeText;
    [SerializeField] TextMeshProUGUI costText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        roleText.text = squadFormation.squadData.leaderUnit.role.ToString();
        soldierCntText.text = squadFormation.squadData.squadMemberCnt.ToString();
        hpText.text = squadFormation.squadData.leaderUnit.hp.ToString("F1");
        atkText.text = squadFormation.squadData.leaderUnit.atk.ToString("F1");
        virusPowText.text = squadFormation.squadData.leaderUnit.virusPow.ToString("F1");
        atkSpdText.text = squadFormation.squadData.leaderUnit.atkSpd.ToString("F1");
        spdText.text = squadFormation.squadData.leaderUnit.spd.ToString("F1");

        if(squadFormation.squadData.leaderUnit.isFly)
        {
            isFlyText.text = "Sky";
        }
        else
        {
            isFlyText.text = "Ground";
        }

        rangeText.text = squadFormation.squadData.leaderUnit.range.ToString();
        costText.text = squadFormation.squadData.leaderUnit.cost.ToString();
    }
}
