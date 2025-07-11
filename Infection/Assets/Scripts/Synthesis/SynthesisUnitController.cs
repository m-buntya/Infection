using System.Data;
using System.Net;
using UnityEngine;

namespace StatePatteren.State
{
    public class SynthesisUnitController : MonoBehaviour
    {
        public enum UNIT_GROUP
        {
            PLAYER,
            ENEMY,
        }

        SynthesisUnitController synthesisUnitController;
        UnitFormation unitFormation;
        UnitManager unitManager;

        public SynthesisUnitStats synthesisUnitStats { get; private set; }

        private SquadStateMachine stateMachine;

        public SquadStateMachine StateMachine => stateMachine;


        UNIT_GROUP unitGroup;

        public void SetUnitStats(SynthesisUnitStats stats)
        {
            synthesisUnitStats = stats;
        }

        public void SetUnitGroup(UNIT_GROUP group)
        {
            unitGroup = group;
        }

        public UNIT_GROUP GetUnitGroup()
        {
            return unitGroup;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            unitFormation = GameObject.Find("UnitFormation").GetComponent<UnitFormation>();
            unitManager = GameObject.Find("UnitManager").GetComponent<UnitManager>();
            //stateMachine = new SquadStateMachine(this);

            stateMachine.Initialize(stateMachine.moveState);
        }

        // Update is called once per frame
        void Update()
        {
            stateMachine.Update();
            stateMachine.Transition();

            if (synthesisUnitStats.hp <= 0)
            {
                Dead();
            }
        }

        // ダメージ処理
        public void TakeDamage(float damage)
        {
            synthesisUnitStats.hp -= damage;
            Debug.Log($"Unit：{damage}のダメージを受けた");
        }

        // 感染ゲージ増加処理
        //public void TakeVirusDamage(float addPoint, string group)
        //{
        //    if (group == "Enemy")
        //    {
        //        synthesisUnitStats.enemyVirusPoint += addPoint;
        //        Debug.Log($"Unit：敵ウイルスの感染ゲージが{addPoint}上昇した");
        //    }
        //    else
        //    {
        //        synthesisUnitStats.virusPoint += addPoint;
        //        Debug.Log($"Unit：自ウイルスの感染ゲージが{addPoint}上昇した");
        //    }
        //}

        // 回復処理
        public void CareHp(float hp)
        {
            synthesisUnitStats.hp = Mathf.Max(synthesisUnitStats.hp + hp, synthesisUnitStats.maxHp);

            Debug.Log($"Unit：体力が{hp}回復した");
        }

        // 感染回復処理
        //public void CarevirusPoint(float carePoint, string group)
        //{
        //    if (group == "Enemy")
        //    {
        //        synthesisUnitStats.enemyVirusPoint -= carePoint;
        //        Debug.Log($"Unit：敵ウイルスの感染ゲージが{carePoint}減少した");
        //    }
        //    else
        //    {
        //        synthesisUnitStats.virusPoint -= carePoint;
        //        Debug.Log($"Unit：自ウイルスの感染ゲージが{carePoint}減少した");
        //    }
        //}

        // 壊滅処理
        void Dead()
        {
            Debug.Log("死亡処理開始");

            if (unitGroup == UNIT_GROUP.PLAYER)
            {
                unitManager.RemoveUnitList(gameObject, "Player");
            }
            else
            {
                unitManager.RemoveUnitList(gameObject, "Enemy");
            }

            Destroy(gameObject);
        }
    }

}