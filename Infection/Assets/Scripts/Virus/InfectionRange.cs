using StatePatteren.State;
using UnityEditorInternal;
using UnityEngine;
using static StatePatteren.State.UnitController;

public class InfectionRange : MonoBehaviour
{
    public float infectionRadius = 5f;
    public float infectionPerTick = 10f;
    public float tickInterval = 1f;
    private UNIT_GROUP selfGroup;

    //初動処理（機能停止）
    private void Awake()
    {
        //this.enabled = false;
    }

    //グループ取得並びに範囲検知から持続的にウイルス感染拡大
    private void Start()
    {
        selfGroup = GetComponent<UnitController>().GetUnitGroup();
        InvokeRepeating("DealInfection", 0f, tickInterval);
    }

    //距離内の敵ユニットにウイルスを感染させる
    void DealInfection()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, infectionRadius);

        foreach (Collider2D hit in hits)
        {
            UnitController status = hit.GetComponent<UnitController>();
            if (status == null) continue;
            if (status.GetUnitGroup() != selfGroup)
            {
                Debug.Log("感染呼び出し：" + hit.name);
                status.TakeVirusDamage(infectionPerTick, selfGroup.ToProperGroupString());
            }
        }
    }
}
