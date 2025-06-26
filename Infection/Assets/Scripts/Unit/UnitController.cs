using System.Net;
using UnityEngine;

namespace StatePatteren.State
{
    public class UnitController : MonoBehaviour
    {
        UnitController unitController;

        public UnitStats unitStats { get; private set; }

        private SquadStateMachine stateMachine;
        public SquadStateMachine StateMachine => stateMachine;

        public UnitFormation unitFormation;
         
        public bool isGuard { get; private set; } = false;

        public void SetUnitStats(UnitStats stats)
        {
            unitStats = stats;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            unitFormation = GameObject.Find("UnitFormation").GetComponent<UnitFormation>();
            stateMachine = new SquadStateMachine(this);

            stateMachine.Initialize(stateMachine.moveState);
        }

        // Update is called once per frame
        void Update()
        {
            stateMachine.Update();
            stateMachine.Transition();
        }

        // ダメージ処理
        public void TakeDamage(float damage)
        {
            if(isGuard)
            {
                Debug.Log("ダメージを無効化");
            }
            else
            {
                unitStats.hp -= damage;
            }

            Debug.Log($"Unit：{damage}のダメージを受けた");

            if (unitStats.hp <= 0)
            {
                Dead();
            }
        }

        // 感染ゲージ増加処理
        public void TakeVirusDamage(float addPoint, string type)
        {
            if (type == "Enemy")
            {
                unitStats.enemyVirusPoint += addPoint;
                Debug.Log($"Unit：敵ウイルスの感染ゲージが{addPoint}上昇した");
            }
            else
            {
                unitStats.virusPoint += addPoint;
                Debug.Log($"Unit：自ウイルスの感染ゲージが{addPoint}上昇した");
            }            
        }

        // 回復処理
        public void CareHp(float hp)
        {
            unitStats.hp = Mathf.Max(unitStats.hp + hp, unitStats.maxHp);

            Debug.Log($"Unit：体力が{hp}回復した");
        }

        // 感染回復処理
        public void CarevirusPoint(float carePoint, string type)
        {
            if (type == "Enemy")
            {
                unitStats.enemyVirusPoint -= carePoint;
                Debug.Log($"Unit：敵ウイルスの感染ゲージが{carePoint}減少した");
            }
            else
            {
                unitStats.virusPoint -= carePoint;
                Debug.Log($"Unit：自ウイルスの感染ゲージが{carePoint}減少した");
            }
        }

        // ガード処理
        public void Guard()
        {
            isGuard = true;
        }

        // 壊滅処理
        public void Dead()
        {
            Destroy(gameObject);
        }
    }

}