using UnityEngine;

public class InfectionRangeVirus : MonoBehaviour
{
    float magnificationInfectionRange = 1.5f;
    float? baseRange = null;

    /*起爆用
    bool isEquip = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            EquipInfectionAura(this.gameObject);
            isEquip = true;
        }
        if (Input.GetKeyDown(KeyCode.R) && isEquip == true)
        {
            EquipWidenAura();
        }
    }*/

    //全ウイルス共通感染処理開始
    public void EquipInfectionAura(GameObject player)
    {
        InfectionRange range = player.GetComponent<InfectionRange>();
        if (range != null)
        {
            range.enabled = true;
        }
    }

    //感染範囲拡大
    public void EquipWidenAura()
    {
        InfectionRange aura = GetComponent<InfectionRange>();
        if (baseRange == null)
        {
            baseRange = aura.infectionRadius;
        }
        if (aura != null)
        {
            aura.infectionRadius = baseRange.Value * magnificationInfectionRange;
        }
    }
}
