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
        roleText.text = squadFormation.squadStats.leaderUnit.role.ToString();
        soldierCntText.text = squadFormation.squadStats.squadMemberCnt.ToString();
        hpText.text = squadFormation.squadStats.leaderUnit.hp.ToString("F1");
        atkText.text = squadFormation.squadStats.leaderUnit.atk.ToString("F1");
        virusPowText.text = squadFormation.squadStats.leaderUnit.virusPow.ToString("F1");
        atkSpdText.text = squadFormation.squadStats.leaderUnit.atkSpd.ToString("F1");
        spdText.text = squadFormation.squadStats.leaderUnit.spd.ToString("F1");

        if(squadFormation.squadStats.leaderUnit.isFly)
        {
            isFlyText.text = "Sky";
        }
        else
        {
            isFlyText.text = "Ground";
        }

        rangeText.text = squadFormation.squadStats.leaderUnit.range.ToString();
        costText.text = squadFormation.squadStats.leaderUnit.cost.ToString();
    }
}
